using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ConsoleProject1.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieGame
{
    public class GameSystem
    {
        private bool running = true;
        private int width = 30;
        private int hight = 20;
        private int invenHight = 5;
        
        private List<Isbeing> objects = null;
        private List<Zombie> zombies;
        private User user;
        private Map map;
        private ConsoleKey userInput;
        private Ui ui = new Ui();

        public GameSystem()
        {
            map = new Map(width, hight, invenHight);
            objects = new List<Isbeing>();
            zombies = new List<Zombie>();
           
            user = new User(15, 10, new Inventory(width, hight, invenHight, objects));
            Item baseWeapon = new Item(1, hight, WeaponType.PISTOL);
            user.GetInventory().AddItem(baseWeapon);
           
            objects.Add(user);
            objects.Add(baseWeapon);
            InitPath(map);
            InitObject(map);
        }

        public void Start()
        {
            ui.Start();
        }

        public void Render()
        {
            Console.Clear();
            ui.DrawMap(map);
        }

        public void Input()
        {
            bool check = false;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Black;
            do
            {
                userInput = Console.ReadKey().Key;
                if (((int)userInput >= 49 && (int)userInput <= 51) || ((int)userInput >= 37 && (int)userInput <= 40))
                {
                    if ((int)userInput >= 49 && (int)userInput <= 51)
                    {
                        if (user.GetInventory().HaveItem((WeaponType)(userInput - 48),true))
                        {
                            check = false;
                        }
                        else
                        {
                            check = true;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    check = true;
                }
            } while (check);
        }

        public void Update()
        {
            Attack();
            Move();
            Crash();
            map.UpdateObjects(objects);
        }

        public void Crash()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is Item)
                {
                    if (objects[i].GetPosition() == user.GetPosition())
                    {
                        user.GetInventory().AddItem((Item)objects[i]);
                    }
                }
                
            }
        }

        public bool GetRunning()
        {
            return running;
        }
       
       
        private void InitObject(Map map)
        {
            AddItems();
            AddZombies();
            AddWall();

            List<Pixel> paths = map.GetMap();
            foreach (Isbeing isbeing in objects)
            {
                if (isbeing.IsExistence())
                {
                    paths[isbeing.GetPosition().GetX() + isbeing.GetPosition().GetY() * width].SetIsBeing(isbeing);
                }
            }
        }

        private void InitPath(Map map)
        {
            List<Pixel> paths = map.GetMap();
            for (int i = 0; i < hight; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    paths[j + width * i] = new Pixel(new Vec2(j, i));
                    if (i - 1 >= 0)
                    {
                        paths[j + width * i].SetPath(paths[j + width * (i-1)]);
                    }

                    if (j + 1 < width)
                    {
                        paths[j + width * i].SetPath(paths[(j + 1) + width * i]);
                    }

                    if (i + 1 < hight)
                    {
                        paths[j + width * i].SetPath(paths[j + width * (i + 1)]);
                    }

                    if (j - 1 >= 0)
                    {
                        paths[j + width * i].SetPath(paths[(j - 1) + width * i]);
                    }
                }
            }
        }

        private void AddWall()
        {
            // 맵 영역
            for (int i = 0; i < width; i++)
            {
                objects.Add(new Wall(i, 0));
                objects.Add(new Wall(i, hight - 1));
                objects.Add(new Wall(i, hight + invenHight - 1));
            }

            for (int i = 0; i < hight + invenHight; i++)
            {
                objects.Add(new Wall(0, i));
                objects.Add(new Wall(width - 1, i));
            }


            // 벽
            objects.Add(new Wall(2, 1));
            objects.Add(new Wall(1, 2));
            objects.Add(new Wall(width - 3, 2));
            objects.Add(new Wall(width - 3, 3));
            objects.Add(new Wall(width - 3, 4));
            objects.Add(new Wall(width - 3, 5));
            objects.Add(new Wall(width - 3, 6));
            objects.Add(new Wall(width - 2, hight - 3));
            objects.Add(new Wall(width - 3, hight - 3));
            objects.Add(new Wall(width - 4, hight - 3));
            objects.Add(new Wall(width - 5, hight - 3));
            objects.Add(new Wall(width - 6, hight - 3));
            objects.Add(new Wall(width - 7, hight - 3));
            objects.Add(new Wall(width - 8, hight - 3));
            objects.Add(new Wall(width - 9, hight - 3));
            objects.Add(new Wall(width - 10, hight - 3));
            objects.Add(new Wall(width - 11, hight - 3));
            objects.Add(new Wall(width - 12, hight - 3));
            objects.Add(new Wall(width - 13, hight - 3));
            objects.Add(new Wall(width - 14, hight - 3));
            objects.Add(new Wall(width - 15, hight - 3));
        }

        private void AddItems()
        {
            Item item1 = new Item(10, 10, WeaponType.PISTOL);
            Item item2 = new Item(5, 6, WeaponType.RIFLE);
            Item item3 = new Item(12, 4, WeaponType.SHOTGUN);

         
            objects.Add(item1);
            objects.Add(item2);
            objects.Add(item3);
        }

        private void AddZombies()
        {
            Zombie zombie1 = new Zombie(1, 1);
            Zombie zombie2 = new Zombie(width - 2, 3);
            Zombie zombie3 = new Zombie(1, hight -2 );
            Zombie zombie4 = new Zombie(width - 2, hight - 2);

            zombies.Add(zombie1);
            zombies.Add(zombie2);
            zombies.Add(zombie3);
            zombies.Add(zombie4);
            objects.Add(zombie1);
            objects.Add(zombie2);
            objects.Add(zombie3);
            objects.Add(zombie4);
        }

        private void Attack()
        {
            int sight = 0;
            int zombieCount = 0;
            switch (userInput)
            {

                // 권총 
                case ConsoleKey.D1:
                    if (user.GetInventory().HaveItem(WeaponType.PISTOL))
                    {
                        sight = 1;
                        zombieCount = 1;
                    }
                    break;

                // 소총
                case ConsoleKey.D2:
                    if (user.GetInventory().HaveItem(WeaponType.RIFLE))
                    {
                        sight = 2;
                        zombieCount = 1;
                    }
                    break;

                //샷건
                case ConsoleKey.D3:
                    if (user.GetInventory().HaveItem(WeaponType.SHOTGUN))
                    {
                        sight = 2;
                        zombieCount = 2;
                    }
                    break;
            }

            if (sight != 0)
            {
                for (int i = 0; i < zombies.Count; i++)
                {

                    if (zombieCount == 0)
                    {
                        break;
                    }

                    if (zombies[i].IsExistence() && shoot(sight, zombies[i].GetPosition()))
                    {
                        zombies[i].Disapear();
                        zombieCount--;

                    }

                }
            }

        }
        private bool shoot(int sight, Vec2 zombiePos)
        {
            int playerX = user.GetPosition().GetX();
            int playerY = user.GetPosition().GetY();


            return (CalcManhattan(Math.Abs(zombiePos.GetX() - playerX), Math.Abs(zombiePos.GetY() - playerY)) <= sight * 2);
        }
        private int CalcManhattan(int diffX, int diffY)
        {
            return diffX + diffY;
        }

        private void Move()
        {
            switch (userInput)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    user.MoveUp(map);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    user.MoveDown(map);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    user.MoveLeft(map);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    user.MoveRight(map);
                    break;
            }
        }
    }
}
