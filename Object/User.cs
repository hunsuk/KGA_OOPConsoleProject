using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieGame.Enum;
using ZombieGame.Utility;

namespace ZombieGame.Object
{
    public class User : People
    {
        private Inventory inventory;

        public User(int x, int y, Inventory inventory) : base(x, y)
        {
            this.inventory = inventory;
        }

        public Inventory GetInventory()
        {
            return inventory;
        }

        public override char GetSign()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return 'U';
        }


        public void Update(Map map, ConsoleKey userInput)
        {
            Attack(map, userInput);
        }



        public void Attack(Map map, ConsoleKey userInput)
        {
            int sight = 0;
            int zombieCount = 0;

            Equip(ref sight, ref zombieCount, userInput);
            
            if (sight != 0)
            {
                List<Pixel> pixels = map.GetMapBySight(sight, GetPosition());
            
                for (int i = 0; i < pixels.Count; i++)
                {
                    if (zombieCount == 0)
                    {
                        break;
                    }

                    if (pixels[i].GetIsbeing() is Zombie)
                    {
                        ((Zombie)pixels[i].GetIsbeing()).Disapear();
                        zombieCount--;
                    }
                }
            }
        }



        private void Equip(ref int sight, ref int zombieCount, ConsoleKey userInput)
        {
            switch (userInput)
            {

                // 권총 
                case ConsoleKey.D1:
                    if (GetInventory().HaveItem(WeaponType.PISTOL))
                    {
                        sight = 1;
                        zombieCount = 1;
                    }
                    break;

                // 소총
                case ConsoleKey.D2:
                    if (GetInventory().HaveItem(WeaponType.RIFLE))
                    {
                        sight = 2;
                        zombieCount = 1;
                    }
                    break;

                //샷건
                case ConsoleKey.D3:
                    if (GetInventory().HaveItem(WeaponType.SHOTGUN))
                    {
                        sight = 2;
                        zombieCount = 2;
                    }
                    break;
            }
        }
    }
}
