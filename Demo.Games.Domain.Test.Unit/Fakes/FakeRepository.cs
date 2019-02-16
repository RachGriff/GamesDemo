using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.Games.Domain.Adapters;
using Demo.Games.Domain.Models;
using MongoDB.Bson;

namespace Demo.Games.Test.Fakes
{
    public class FakeRepository:IGamesRepository
    
    {
        public IList<Game> Games;
        public FakeRepository()
        {
            Games=new List<Game>();
        }

        public string Create(string name, string description, DateTimeOffset released, int rating)
        {
            var newGame = new Game
            {
                Id = ObjectId.GenerateNewId(),
                Name = name,
                Description = description,
                Released = released,
                Rating = rating

            };
            Games.Add(newGame);
           return newGame.Id.ToString();
        }

        public bool Update(Game game)
        {
           
            var gameToUpdate = Games.FirstOrDefault(g => g.Id == game.Id);
            if (gameToUpdate==null) return false;
            Games.Remove(gameToUpdate);
            Games.Add(game);
            return true;
        }

        public ICollection<Game> GetAll()
        {
            return Games;
        }

        public Game GetById(string id)
        {
            var game = Games.FirstOrDefault(g => g.Id.ToString() == id.ToString());
            if(game == null) return null;

            return new Game
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Released = game.Released,
                Rating = game.Rating
            };
        }

        public bool Delete(string id)
        {
            var gameToDelete = Games.FirstOrDefault(g => g.Id.ToString() ==id.ToString());
            if (gameToDelete == null) return false;
            Games.Remove(gameToDelete);
            return true;
        }
    }
}
