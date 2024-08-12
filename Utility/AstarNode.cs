using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame.Utility
{
    public class AstarNode
    {
        public Vec2 position;
        public double Fscore;
        public double Gscore;
        public double Hscore;
        public AstarNode parent;

        public AstarNode(Vec2 position, double Hscore, double Gscore, AstarNode parent)
        {
            this.position = position;
            this.Fscore = 1 + Hscore;
            this.Gscore = Gscore;
            this.Hscore = Hscore;
            this.parent = parent;
        }

        public override bool Equals(object? obj)
        {
            return this.GetHashCode() == obj?.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (this.position.GetX().ToString() + this.position.GetY().ToString()).GetHashCode();
        }

    }
}
