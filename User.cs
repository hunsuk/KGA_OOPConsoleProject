using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieGame
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


    }
}
