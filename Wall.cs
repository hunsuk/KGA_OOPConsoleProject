using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public class Wall : Isbeing
    {
        private Vec2 position;
        private bool existence;

        public Wall(int x, int y)
        {
            this.position = new Vec2(x, y);
            existence = true;
        }

        public Vec2 GetPosition()
        {
            return position;
        }

        public char GetSign()
        {
            Console.ForegroundColor = ConsoleColor.White;
            return '#';
        }

        public bool IsExistence()
        {
            return existence;
        }
    }
}
