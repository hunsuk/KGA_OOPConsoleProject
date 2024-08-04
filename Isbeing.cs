using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public interface Isbeing
    {
        public bool IsExistence();
        public Vec2 GetPosition();

        public char GetSign();

    }
}
