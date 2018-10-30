using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    public delegate void ButtonClicked(string sender);
    public delegate void GameStarting(List<string> names);

    public interface IButtonClicked
    {
        event ButtonClicked GameButtonClicked;
    }
}
