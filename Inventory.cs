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
        private List<Item> items = new List<Item>();
        private int width;
        private int hight;
        private int invenHight;
        public Inventory(int width, int hight, int invenHight, List<Isbeing> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is Item)
                {
                    this.items.Add((Item)items[i]);
                }
            }

            this.width = width;
            this.hight = hight;
            this.invenHight = invenHight;
        }

        private void SortItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                int width = (i + 1) % this.width;
                int hight = (i + 1) / this.width;
                items[i].SetPostion(width, this.hight + hight);
            }
        }
        

        public void AddItem(Item item)
        {
            items.Add(item);
            if (items.Count == 0)
            {
                item.SetPostion(1, hight);
            } else
            {
                SortItems();
            }
        }

        public bool HaveItem(WeaponType weapon, bool inputCheck = false)
        {
            foreach(Item target in items)
            {
                if (target.IsExistence() && (weapon == target.GetWeapon()))
                {
                    if (!inputCheck)
                    {
                        target.Disapear();
                        items.Remove(target);
                        SortItems();
                    }
                    return true;
                }
            }
            return false;
        }
    }   
}
 