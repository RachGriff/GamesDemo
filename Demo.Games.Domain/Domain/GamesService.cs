using System;
using System.Collections.Generic;
using Demo.Games.Domain.Adapters;
using Demo.Games.Domain.Models;
using MongoDB.Bson;

namespace Demo.Games.Domain.Domain
{
    public class GamesService : IGamesService
    {
        public IGamesRepository _repository;

       
  
        public GamesService(IGamesRepository repository)
        {
            _repository = repository;
            
        }

        public ICollection<Game> GetAll()
        {
            var result = _repository.GetAll();
            return result;
        }

        public Game GetById(string id)
        {
            var result = _repository.GetById(id);
            return result;
        }

        public string Create(string name, string description, DateTimeOffset released, int rating)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || rating <= 0 ||
                rating > 5) return null;
            return _repository.Create(name, description, released, rating);
        }

        public bool Update(Game game)
        {
            if (string.IsNullOrWhiteSpace(game.Name) || string.IsNullOrWhiteSpace(game.Description) || game.Rating <= 0 ||
                game.Rating > 5) return false;
            return _repository.Update(game);
        }

        
        public bool Delete(string id)
        {
            if (id == null) return false;
            var result = _repository.Delete(id);
            return result;
        }
    }
}
