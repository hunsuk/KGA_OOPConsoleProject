using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public class Pixel
    {
        private Vec2 postion;
        private List<Pixel> path;
        private int weight;
        private Isbeing isbeing = null;

        public Pixel(Vec2 postion, int weight = 1)
        {
            this.postion = postion;
            this.weight = weight;
            this.path = new List<Pixel>();
        }
        
        public Isbeing GetIsbeing()
        {
            return isbeing;
        }

        public bool IsGo()
        {
            return !(this.isbeing is Wall);
        }

        public void SetWeight(int weight)
        {
            this.weight = weight;
        }

        public void SetPath(Pixel pixel)
        {
            path.Add(pixel);
        }
        
        public void SetIsBeing(Isbeing isbeing)
        {
            this.isbeing = isbeing;
        }

    }
}
