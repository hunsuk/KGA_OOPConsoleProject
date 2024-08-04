using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public class Zombie : People
    {
        public Zombie(int x, int y) : base(x, y)
        {

        }

        public override char GetSign()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return 'Z';
        }
    }
}
