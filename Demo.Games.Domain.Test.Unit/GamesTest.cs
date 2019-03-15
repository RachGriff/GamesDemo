using System;
using System.Collections.Immutable;
using System.Runtime.Serialization;
using NUnit.Framework;
using Demo.Games.Test.Fakes;
using Demo.Games.Domain.Domain;
using Demo.Games.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Demo.Games.Test
{

    public abstract class GamesServiceBaseClass
    {
        protected IGamesService _service;
        protected FakeRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new FakeRepository();
            _service = new GamesService(_repository);
        }
    }



    [TestFixture]
    public class A_list_of_games_will_be_returned : GamesServiceBaseClass
    {
        [Test]
        public void when_the_there_are_games_in_the_data_store()
        {
            _repository.Games.Add(new Game
            {
                Id = ObjectId.GenerateNewId(),
                Name = "My new Game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });
            var games = _service.GetAll();
            Assert.Greater(games.Count, 0);
        }
    }

    [TestFixture]
    public class A_list_of_games_will_not_be_returned : GamesServiceBaseClass
    {
        [Test]
        public void when_there_are_no_games_in_the_games_store()
        {
            var games = _service.GetAll();
            Assert.AreEqual(0, games.Count);
        }
    }

    [TestFixture]
    public class A_game_will_be_returned : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_game_id_is_provided()
        {
            var id = ObjectId.GenerateNewId();
            _repository.Games.Add(new Game
            {
                Id = id,
                Name = "My new game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });

            var game = _service.GetById(id.ToString());
            Assert.IsNotNull(game);
            Assert.AreEqual("My new game", game.Name);



        }
    }

    [TestFixture]
    public class A_game_will_not_be_returned : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_id_is_not_provided()
        {
            _repository.Games.Add(new Game
            {
                Id = ObjectId.GenerateNewId(),
                Name = "My new Game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });

            Assert.IsNull(_service.GetById(""));
        }
    }

    [TestFixture]
    public class A_new_game_will_be_created : GamesServiceBaseClass
    {
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void when_a_valid_name_description_release_date_and_rating_are_provided(int rating)
        {
            var result = _service.Create("My new game", "Its is really great", DateTimeOffset.Now, rating);

            Assert.IsNotNull(result);
        }
    }

    [TestFixture]
    public class A_new_game_will_not_be_created : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_name_is_not_provided()
        {
            var result = _service.Create("", "It's is really great", DateTimeOffset.Now, 5);

            Assert.IsNull(result);
        }


        [Test]
        public void when_a_valid_description_is_not_provided()
        {
            var result = _service.Create("My new game", "", DateTimeOffset.Now, 5);

            Assert.IsNull(result);
        }


        [Test]
        [TestCase(0)]
        [TestCase(11)]
        public void when_a_valid_rating_of_between_1_and_10_is_not_provided(int rating)
        {
            var result = _service.Create("", "Its is really great", DateTimeOffset.Now, rating);

            Assert.IsNull(result);
        }
    }

    [TestFixture]
    public class A_game_will_be_updated : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_name_is_provided()

        {
            var id = ObjectId.GenerateNewId();
            _repository.Games.Add(new Game
            {
                Id = id,
                Name = "My new Game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });
            var gameToUpdate = _service.GetById(id.ToString());
            gameToUpdate.Name = "My new even better game";
            var upDatedGame = _service.Update(gameToUpdate);
            Assert.IsTrue(upDatedGame);
            gameToUpdate = _service.GetById(id.ToString());
            Assert.AreEqual(gameToUpdate.Name, "My new even better game");
        }

        [Test]
        public void when_a_valid_description_is_provided()

        {
            var id = ObjectId.GenerateNewId();
            _repository.Games.Add(new Game
            {
                Id = id,
                Name = "My new Game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });
            var gameToUpdate = _service.GetById(id.ToString());
            gameToUpdate.Description = "It's a roller coaster of a thrill to play!";
            var upDatedGame = _service.Update(gameToUpdate);
            Assert.IsTrue(upDatedGame);
            gameToUpdate = _service.GetById(id.ToString());
            Assert.AreEqual(gameToUpdate.Description, "It's a roller coaster of a thrill to play!");
        }

        [Test]
        public void when_a_valid_rating_is_provided()

        {
            var id = ObjectId.GenerateNewId();
            _repository.Games.Add(new Game
            {
                Id = id,
                Name = "My new Game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });
            var gameToUpdate = _service.GetById(id.ToString());
            gameToUpdate.Rating = 4;
            var upDatedGame = _service.Update(gameToUpdate);
            Assert.IsTrue(upDatedGame);
            gameToUpdate = _service.GetById(id.ToString());
            Assert.AreEqual(gameToUpdate.Rating, 4);
        }
    }

    [TestFixture]
    public class A_game_will_not_be_updated : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_name_is_not_provided()
        {
            {
                var id = ObjectId.GenerateNewId();
                _repository.Games.Add(new Game
                {
                    Id = id,
                    Name = "My new game",
                    Description = "Exciting new Challenge",
                    Released = DateTimeOffset.Now,
                    Rating = 5,
                });
                var gameToUpdate = _service.GetById(id.ToString());
                gameToUpdate.Name = "";
                var upDatedGame = _service.Update(gameToUpdate);
                Assert.IsFalse(upDatedGame);

            }

        }

        [Test]
        public void when_a_valid_description_is_not_provided()
        {
            {
                var id = ObjectId.GenerateNewId();
                _repository.Games.Add(new Game
                {
                    Id = id,
                    Name = "My new game",
                    Description = "Exciting new Challenge",
                    Released = DateTimeOffset.Now,
                    Rating = 5,
                });
                var gameToUpdate = _service.GetById(id.ToString());
                gameToUpdate.Description = "";
                var upDatedGame = _service.Update(gameToUpdate);
                Assert.IsFalse(upDatedGame);

            }

        }

        [Test]
        public void when_a_valid_rating_of_between_1_and_10_is_not_provided()
        {
            {
                var id = ObjectId.GenerateNewId();
                _repository.Games.Add(new Game
                {
                    Id = id,
                    Name = "My new game",
                    Description = "Exciting new Challenge",
                    Released = DateTimeOffset.Now,
                    Rating = 5,
                });
                var gameToUpdate = _service.GetById(id.ToString());
                gameToUpdate.Rating = 0;
                var upDatedGame = _service.Update(gameToUpdate);
                Assert.IsFalse(upDatedGame);

            }

        }
    }

    [TestFixture]
    public class A_game_will_be_deleted : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_id_is_provided()
        {

            var id = ObjectId.GenerateNewId();
            _repository.Games.Add(new Game
            {
                Id = id,
                Name = "My new game",
                Description = "Exciting new Challenge",
                Released = DateTimeOffset.Now,
                Rating = 5,
            });
            var gameToDelete = _service.GetById(id.ToString());
            var result = _service.Delete(gameToDelete.Id.ToString());
            Assert.IsTrue(result);
            var deletedGameResult = _service.GetById(gameToDelete.Id.ToString());
            Assert.IsNull(deletedGameResult);

        }
    }

    [TestFixture]
    public class A_game_will_not_be_deleted : GamesServiceBaseClass
    {
        [Test]
        public void when_a_valid_id_is_not_provided()
        {

     
            var result = _service.Delete("");
            Assert.IsFalse(result);


        }
    }
}


