using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Interface;
using ZombieGame.Utility;

namespace ZombieGame.Object
{
    public class Wall : Isbeing
    {
        private Vec2 position;
        private bool existence;

        public Wall(int x, int y)
        {
            position = new Vec2(x, y);
            existence = true;
        }

        public void Disapear()
        {
            existence = false;
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
