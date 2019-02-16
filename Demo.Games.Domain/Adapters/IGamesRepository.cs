using System;
using System.Collections.Generic;
using System.Text;
using Demo.Games.Domain.Models;
using MongoDB.Bson;

namespace Demo.Games.Domain.Adapters
{
    public interface IGamesRepository


    {
        ICollection<Game> GetAll();
        Game GetById(string id);
        string Create(string name, string description, DateTimeOffset released, int rating);
        bool Update(Game game);
        bool Delete(string id);
    }
}
