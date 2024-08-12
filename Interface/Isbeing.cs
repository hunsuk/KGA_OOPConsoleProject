using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Utility;

namespace ZombieGame.Interface
{
    public interface Isbeing
    {
        public bool IsExistence();
        public Vec2 GetPosition();

        public char GetSign();

        public void Disapear();
    }
}
