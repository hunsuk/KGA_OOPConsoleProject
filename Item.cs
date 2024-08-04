using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
{
    public class Item : Isbeing
    {
        private Vec2 postion;
        private bool existence;
        private WeaponType type;

        public Item(int x, int y, WeaponType type)
        {
            this.postion = new Vec2(x, y);
            this.type = type;
            existence = true;
        }

        public Vec2 GetPosition()
        {
            return postion;
        }

        public char GetSign()
        {
            switch(type)
            {
                case WeaponType.PISTOL:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    return 'P';
                case WeaponType.RIFLE:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    return 'R';
                case WeaponType.SHOTGUN:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return 'S';
                default:
                    return ' ';
            }
        }

        public bool IsExistence()
        {
            return existence;
        }
    }
}
