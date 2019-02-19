using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScope.Games.api.ViewModels
{
    public class GameRepresentation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Released { get; set; }
        public int Rating { get; set; }
    }
}
