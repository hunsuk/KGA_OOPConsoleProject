using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame.Equipment
{
    public class Weapon
    {
        public int sight;
        public int range;

        public Weapon(int sight, int range)
        {
            this.sight = sight;
            this.range = range;
        }
    }
}
