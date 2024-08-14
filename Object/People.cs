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
        protected Vec2 postion;
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

        

        public void SetPostion(int width, int hight)
        {
            postion.SetX(width);
            postion.SetY(hight);
        }

        public bool Move()
        {
            throw new NotImplementedException();
        }
    }
}
