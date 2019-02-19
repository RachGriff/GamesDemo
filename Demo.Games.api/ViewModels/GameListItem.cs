using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Games.API.ViewModels
{
    public class GameListItem
    { 
       public string Id { get; set; }
       public string Name { get; set; }
       public string Released { get; set; }
       public int Rating { get; set; }
    }
}
