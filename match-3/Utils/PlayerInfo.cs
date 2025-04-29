using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Utils
{
    public static class PlayerInfo
    {
        public static int Score { get; set; } = 0;

        public static void Reset()
        {
            Score = 0;
        }
    }
}