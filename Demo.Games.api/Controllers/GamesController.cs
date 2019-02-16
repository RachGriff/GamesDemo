using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataScope.Games.api.ViewModels;
using Demo.Games.API.ViewModels;
using Demo.Games.Domain.Domain;
using Demo.Games.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MongoDB.Bson;

namespace Demo.Games.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGamesService _service;

        public GamesController(IGamesService service)
        {
            _service = service;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<ICollection<GameListItem>> Get()
        {
            return _service.GetAll().Select(g => new GameListItem {Id=g.Id.ToString(), Name=g.Name, Released=g.Released, Rating=g.Rating}).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<GameRepresentation> Get(string id)
        {
            var game = _service.GetById(id);

            if (game == null) return BadRequest();
            return new GameRepresentation
            {
                Id = game.Id.ToString(), Name = game.Name, Description = game.Description, Released = game.Released, Rating = game.Rating
            };
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post(string name, string description, DateTimeOffset released, int rating)
        {
            var id = _service.Create(name, description, released, rating);
            if (id == null) return BadRequest();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult <GameRepresentation> Put (string id, [FromBody]string name, string description, DateTimeOffset released, int rating)
        {
            var game = new Game
                { Id = new ObjectId(id), Name = name, Description = description, Released = released, Rating = rating};
            var result = _service.Update(game);
            if (result == false) return BadRequest();
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var result = _service.Delete(id);
            if (result == false) return BadRequest();
            return Ok();
        }
    }
}
