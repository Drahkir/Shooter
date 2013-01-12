using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shooter
{
    class PersistentGameData
    {
        public bool JustWon { get; set; }
        public LevelDescription CurrentLevel { get; set; }

        public PersistentGameData() {
            JustWon = false;
        }
    }
}
