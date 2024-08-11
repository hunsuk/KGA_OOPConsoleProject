using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public class Vec2
    {
        private int x;
        private int y;

        public Vec2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
      
        public static bool operator!= (Vec2 left, Vec2 right)
        {
            return left.x != right.x || left.y != right.y; 
        }
        public static bool operator == (Vec2 left, Vec2 right)
        {
            return left.x == right.x && left.y == right.y;
        }

    }
}
