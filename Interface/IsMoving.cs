using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Utility;

namespace ZombieGame.Interface
{
    public interface IsMoving
    {
        public bool MoveUp(Map map, bool move);
        public bool MoveDown(Map map, bool move);
        public bool MoveLeft(Map map, bool move);
        public bool MoveRight(Map map, bool move);
    }
}
