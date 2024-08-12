using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Interface;
using ZombieGame.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieGame.Object
{
    public abstract class People : Isbeing, IsMoving
    {
        private Vec2 postion;
        private bool existence;

        public People(int x, int y)
        {
            postion = new Vec2(x, y);
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

        public bool MoveUp(Map map, bool move = true)
        {
            if (map.GetMap()[postion.GetX() + map.GetWidth() * (postion.GetY() - 1)].IsGo())
            {
                if (move)
                {
                    map.GetMap()[postion.GetX() + map.GetWidth() * postion.GetY()].SetIsBeing(null);
                    postion.SetY(postion.GetY() - 1);
                }
                return true;
            }
            return false;
        }

        public bool MoveDown(Map map, bool move = true)
        {
            if (map.GetMap()[postion.GetX() + map.GetWidth() * (postion.GetY() + 1)].IsGo())
            {
                if (move)
                {
                    map.GetMap()[postion.GetX() + map.GetWidth() * postion.GetY()].SetIsBeing(null);
                    postion.SetY(postion.GetY() + 1);
                }
                return true;
            }
            return false;
        }

        public bool MoveLeft(Map map, bool move = true)
        {
            if (map.GetMap()[postion.GetX() - 1 + map.GetWidth() * postion.GetY()].IsGo())
            {
                map.GetMap()[postion.GetX() + map.GetWidth() * postion.GetY()].SetIsBeing(null);
                postion.SetX(postion.GetX() - 1);
                return true;
            }
            return false;
        }

        public bool MoveRight(Map map, bool move = true)
        {
            if (map.GetMap()[postion.GetX() + 1 + map.GetWidth() * postion.GetY()].IsGo())
            {
                map.GetMap()[postion.GetX() + map.GetWidth() * postion.GetY()].SetIsBeing(null);
                postion.SetX(postion.GetX() + 1);
                return true;
            }
            return false;
        }

        public void SetPostion(int width, int hight)
        {
            postion.SetX(width);
            postion.SetY(hight);
        }
    }
}
