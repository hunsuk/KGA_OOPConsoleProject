using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public interface IsMoving
    {
        public void MoveUp(Map map);
        public void MoveDown(Map map);
        public void MoveLeft(Map map);
        public void MoveRight(Map map);
    }
}
