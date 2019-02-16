using System;
using System.Collections.Generic;
using Demo.Games.Domain.Models;

namespace Demo.Games.Domain.Adapters
{
    public class GamesRepository : IGamesRepository
    {
        public ICollection<Game> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Game GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public string Create(string name, string description, DateTimeOffset released, int rating)
        {
            throw new NotImplementedException();
        }

        public bool Update(Game game)
        {
            throw new NotImplementedException();
        }

        public bool Update(string name, string description, DateTimeOffset released, int rating)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
