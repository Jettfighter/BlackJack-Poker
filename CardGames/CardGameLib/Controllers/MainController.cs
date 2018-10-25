using CardGameLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLib.Controllers
{
    public static class MainController
    {
        public static Player ConvertStringIntoPlayer(string name)
        {
            Player player = new Player()
            {
                Name = name
            };
            return player;
        }
    }
}
