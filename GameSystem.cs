using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private List<Item> items;
        private User user;
        private Map map;
        private ConsoleKey userInput;
        private Ui ui = new Ui();

        public GameSystem()
        {
            map = new Map(width, hight, invenHight);
            objects = new List<Isbeing>();
            zombies = new List<Zombie>();
            items = new List<Item>();
            user = new User(15, 10);
            objects.Add(user);
            InitPath(map);
            InitObject(map);
        }

        public void Start()
        {
            ui.Start();
        }

        public void Render()
        {
            ui.DrawMap(map);
        }

        public void Input()
        {
            userInput = Console.ReadKey().Key;
        }

        public void Update()
        {

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

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
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
    }
}
