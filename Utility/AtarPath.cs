using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Interface;
using ZombieGame.Object;

namespace ZombieGame.Utility
{
    public class AstarPath
    {
        
        private List<Isbeing> isbeings;
        private User user;
        private Map map;
        public AstarPath(User user, Map map,List<Isbeing> isbeings)
        {
            this.user = user;
            this.map = map;
            this.isbeings = isbeings;
        }

        public void ZombiesMove()
        {
            for (int i = 0; i < isbeings.Count; i++)
            {
                if (isbeings[i] is Zombie)
                {
                    ((Zombie)isbeings[i]).Move(user, map);
                }        
            }
        }

        public static double CalcEuclid(Vec2 zombiePos, Vec2 userPos)
        {
            int diffX = zombiePos.GetX() - userPos.GetX();
            int diifY = zombiePos.GetY() - userPos.GetY();
            return Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diifY, 2));
        }
    }
}
