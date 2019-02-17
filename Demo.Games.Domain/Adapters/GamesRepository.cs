using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.Serialization;
using Demo.Games.Domain.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Demo.Games.Domain.Adapters
{
    public class GamesRepository : IGamesRepository
    {
        private IMongoCollection<Game> _collection;
        public GamesRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Game>("Games");
        }
        public ICollection<Game> GetAll()
        {
            var gameCollection = _collection.Find(FilterDefinition<Game>.Empty);
            return gameCollection.ToList();
        }

        public Game GetById(string id)
        {
            var filterId = Builders<Game>.Filter.Eq("Id",ObjectId.Parse(id));
            return _collection.Find(filterId).FirstOrDefault();
       
        }

        public string Create(string name, string description, DateTimeOffset released, int rating)
        {
            var id = ObjectId.GenerateNewId();
            var newGame = new Game
            {
                Id = id,
                Name = name,
                Description = description,
                Released = released,
                Rating = rating,

            };
                _collection.InsertOne(newGame);
                return newGame.Id.ToString();
        }

        public bool Update(Game game)
        {
            var id = game.Id.ToString();
            var filterId = Builders<Game>.Filter.Eq("Id", ObjectId.Parse(id));
            var result=_collection.ReplaceOne(filterId, game);
            return result.ModifiedCount == 1;
        }

        

        public bool Delete(string id)
        {
            var filterId = Builders<Game>.Filter.Eq("Id", ObjectId.Parse(id));
            var result = _collection.DeleteOne(filterId);
            return result.DeletedCount == 1;
        }
    }
}
