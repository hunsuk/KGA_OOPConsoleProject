using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZombieGame.Enum;
using ZombieGame.Interface;
using ZombieGame.Object;
using ZombieGame.Utility;
using static ConsoleProject1.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;


//Todo
// A* 적용
// 맵 랜더링 초기화

namespace ZombieGame
{
    public class GameSystem
    {
        private bool running = true;
        private bool userWin = false;
        private int width = 30;
        private int hight = 20;
        private int invenHight = 5;
        
        private List<Isbeing> objects = null;
        private User user;
        private Map map;
        private ConsoleKey userInput;
        private ConsoleKey beforInput;
        private Ui ui = new Ui();

        public GameSystem()
        {
            map = new Map(width, hight, invenHight);
            objects = new List<Isbeing>();           
            user = new User(15, 10, new Inventory(width, hight, invenHight, objects));
            Item baseWeapon = new Item(1, hight, WeaponType.PISTOL);
            user.GetInventory().AddItem(baseWeapon);
           
            objects.Add(user);
            objects.Add(baseWeapon);

            InitPath(map);
            InitObject(map);
            AstarPath astarPath = new AstarPath(user, map, objects);
        }

        static double CalcEuclid(int diffX, int diifY)
        {
            return Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diifY, 2));
        }

        public void Start()
        {
            ui.Start();
        }

        public void End()
        {
            ui.End(userWin);
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
                //userInput = Console.ReadKey(true).Key;
                Thread.Sleep(150);
                if (Console.KeyAvailable)
                {
                    userInput = Console.ReadKey(true).Key;
                }
                else
                {
                    break;
                }

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
                        beforInput = userInput;
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

            user.Update(map, userInput, beforInput);
            Crash();
            Chase(ref running);
            map.UpdateObjects(objects);
            //userInput = ConsoleKey.Tab;
        }

        public void Chase(ref bool running)
        {
            // A* 알고리즘
            
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].IsExistence() && objects[i] is Zombie)
                {
                    ((Zombie)objects[i]).Move(user, map);
                }
            }

            if (!user.IsExistence())
                running = false;
        }

        public void Crash()
        {
            if (!running)
            {
                return;
            }
            int zombieCount = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] is Zombie && objects[i].IsExistence())
                {
                    zombieCount++;
                }

                if (objects[i] is Zombie && objects[i].IsExistence())
                {
                    if (AstarPath.CalcEuclid(objects[i].GetPosition(), user.GetPosition()) < 1.45)
                    {
                        userWin = false;
                        running = false;
                        break;
                    }
                }
            }

            if (zombieCount == 0)
            {
                userWin = true;
                running = false;
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
            objects.Add(new Wall(width - 2, 2));
            objects.Add(new Wall(width - 3, 2));
            objects.Add(new Wall(width - 3, 3));
            objects.Add(new Wall(width - 3, 4));
            objects.Add(new Wall(width - 3, 5));
            objects.Add(new Wall(width - 3, 6));
            objects.Add(new Wall(width - 3, 6));
            objects.Add(new Wall(width - 3, 6));
            objects.Add(new Wall(width - 3, 7));
            objects.Add(new Wall(width - 3, 8));
            objects.Add(new Wall(width - 3, 9));
            objects.Add(new Wall(width - 3, 10));
            objects.Add(new Wall(width - 3, 11));
            objects.Add(new Wall(width - 3, 12));
            objects.Add(new Wall(width - 3, 13));
            objects.Add(new Wall(width - 3, 14));
            objects.Add(new Wall(width - 3, 15));
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
            Zombie zombie2 = new Zombie(width - 2, 10);
            Zombie zombie3 = new Zombie(1, hight - 2);
            Zombie zombie4 = new Zombie(width - 2, hight - 2);

            objects.Add(zombie1);
            objects.Add(zombie2);
            objects.Add(zombie3);
            objects.Add(zombie4);
        }
    }
}
