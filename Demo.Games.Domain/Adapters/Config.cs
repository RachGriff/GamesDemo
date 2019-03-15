using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Games.Domain.Adapters
{
    public static class Config
    {
        public static string MongoDbConnectionString = System.Environment.GetEnvironmentVariable("GAMES_CONNECTIONSTRING",EnvironmentVariableTarget.Machine);

        
    }
}
