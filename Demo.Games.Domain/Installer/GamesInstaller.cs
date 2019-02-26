using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Demo.Games.Domain.Adapters;
using Demo.Games.Domain.Domain;
using MongoDB.Driver;


namespace Demo.Games.Domain.Installer
{
    public static class GamesInstaller
    {
        public static void AddGames(this IServiceCollection services)
        {
            var connectionString = "mongodb://rlgriffiths:gamesdemo123@ds123513.mlab.com:23513/gamesdemo";
            var databaseName = "Games";
            var client=new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);


            services.AddSingleton<IMongoDatabase>(database);
            services.AddTransient<IGamesRepository, GamesRepository>();
            services.AddTransient<IGamesService, GamesService>();
        }
    }
}
