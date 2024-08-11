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
        private int width;
        private int hight;
        private int invenHight;
        public Inventory(int width, int hight, int invenHight)
        {
            items = new List<Item>();
            this.width = width;
            this.hight = hight;
            this.invenHight = invenHight;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            if (items.Count == 0)
            {
                item.SetPostion(1, hight);
            } else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    int width = (i + 1) % this.width;
                    int hight = (i + 1) / this.width;
                    items[i].SetPostion(width, this.hight + hight);
                }
            }
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
 