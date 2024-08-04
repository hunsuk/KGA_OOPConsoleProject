using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public class User : People
    {
        public User(int x, int y) : base(x, y) { }

        public override char GetSign()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'U';
        }
    }
}
