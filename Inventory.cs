using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleProject1.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieGame
{
    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>();
            items.Add(new Item(WeaponType.PISTOL));
        }


        

        public bool HaveItem(WeaponType weapon)
        {
            foreach(Item target in items)
            {
                if (weapon == target.GetWeapon())
                {
                    return true;
                }
            }
            return false;
        }
    }   
}
 