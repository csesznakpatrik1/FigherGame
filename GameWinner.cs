using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FighterGame
{
     class GameWinner
    {

        public string WinnerName { get; set; }
        public string LoserName { get; set; }
        public DateTime DateTime { get; set; }


            public GameWinner(string winnerName, string loserName, DateTime dateTime)
        {
            WinnerName = winnerName;
            LoserName = loserName;
            DateTime = dateTime;
        }
    }
}
