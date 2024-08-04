using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public abstract class People : Isbeing
    {
        private Vec2 postion;
        private bool existence;

        public People(int x, int y)
        {
            this.postion = new Vec2(x, y);
            existence = true;
        }

        public void Move()
        {

        }

        public void Attack()
        {

        }

        public bool IsExistence()
        {
            return existence;
        }

        public Vec2 GetPosition()
        {
            return postion;
        }

        public abstract char GetSign();
    }
}
