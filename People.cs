using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieGame
{
    public abstract class People : Isbeing , IsMoving
    {
        private Vec2 postion;
        private bool existence;

        public People(int x, int y)
        {
            this.postion = new Vec2(x, y);
            existence = true;
        }

 
        public void Disapear()
        {
            existence = false;
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

        public void MoveUp(Map map)
        {
            if (map.GetMap()[this.postion.GetX() + map.GetWidth() * (this.postion.GetY() - 1)].IsGo())
            {
                map.GetMap()[this.postion.GetX() + map.GetWidth() * this.postion.GetY()].SetIsBeing(null);
                this.postion.SetY(this.postion.GetY() - 1);
            }
        }

        public void MoveDown(Map map)
        {
            if (map.GetMap()[this.postion.GetX() + map.GetWidth() * (this.postion.GetY() + 1)].IsGo())
            {
                map.GetMap()[this.postion.GetX() + map.GetWidth() * this.postion.GetY()].SetIsBeing(null);
                this.postion.SetY(this.postion.GetY() + 1);
            }
        }

        public void MoveLeft(Map map)
        {
            if (map.GetMap()[this.postion.GetX() -1 + map.GetWidth() * this.postion.GetY()].IsGo())
            {
                map.GetMap()[this.postion.GetX() + map.GetWidth() * this.postion.GetY()].SetIsBeing(null);
                this.postion.SetX(this.postion.GetX() - 1);
            }
        }

        public void MoveRight(Map map)
        {
            if (map.GetMap()[this.postion.GetX() + 1 + map.GetWidth() * this.postion.GetY()].IsGo())
            {
                map.GetMap()[this.postion.GetX() + map.GetWidth() * this.postion.GetY()].SetIsBeing(null);
                this.postion.SetX(this.postion.GetX() + 1);
            }
        }
    }
}
