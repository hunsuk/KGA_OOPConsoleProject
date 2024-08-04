using System.Data;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using ZombieGame;
using static ConsoleProject1.Program;

namespace ConsoleProject1
{
    internal class Program
    {
        public struct GameData
        {
            public bool running;
            public bool[,] map;
            public Item[] items;
            public Inventory inventory;
            public ConsoleKey inputKey;
            public Point playerPos;
            public Zombie[] zombies;
        }

        public struct Zombie
        {
            public bool[,] pass;
            public Point point;
        }

        public struct Item
        {
            public Weapon weapon;
            public Point point;
        }

        public enum Weapon
        {
            INIT, PISTOL, RIFLE, SHOTGUN
        }

        public struct Inventory
        {
            public Weapon[] weapons;
            public int height;
        }

        //2D 위치 자료형 구현
        public struct Point
        {
            public int x;
            public int y;
        }

        static GameData data;

        static void Start()
        {
            int width = 30;
            int hight = 20;
            int invenHight = 5;
            Console.CursorVisible = false;

            data = new GameData();
            data.running = true;

            data.inventory = new Inventory();
            data.inventory.height = hight + 1;
            data.inventory.weapons = new Weapon[10];
            data.inventory.weapons[0] = Weapon.PISTOL;

            data.map = new bool[hight + invenHight, width];

            mapInit(data.map, width, hight, invenHight);


            data.items = new Item[3];
            Item item1 = new Item();
            Item item2 = new Item();
            Item item3 = new Item();

            item1.weapon = Weapon.PISTOL;
            item1.point = new Point() { x = 10, y = 10 };

            item2.weapon = Weapon.RIFLE;
            item2.point = new Point() { x = 5, y = 6 };

            item3.weapon = Weapon.SHOTGUN;
            item3.point = new Point() { x = 12, y = 4 };

            data.items[0] = item1;
            data.items[1] = item2;
            data.items[2] = item3;

            data.playerPos = new Point() { x = 15, y = 10 };
            data.zombies = new Zombie[10];
            data.zombies[0] = new Zombie { pass = new bool[hight + invenHight, width], point = new Point { x = 1, y = 1 } };
            data.zombies[1] = new Zombie { pass = new bool[hight + invenHight, width], point = new Point { x = width - 2, y = 3 } };
            data.zombies[2] = new Zombie { pass = new bool[hight + invenHight, width], point = new Point { x = 1, y = hight - 2 } };
            data.zombies[3] = new Zombie { pass = new bool[hight + invenHight, width], point = new Point { x = width - 2, y = hight - 2 } };

            #region 벽1
            data.map[1, 2] = true;
            data.map[2, 1] = true;
            #endregion
            #region 벽2
            data.map[2, width - 3] = true;
            data.map[3, width - 3] = true;
            data.map[4, width - 3] = true;
            data.map[5, width - 3] = true;
            data.map[6, width - 3] = true;
            #endregion
            #region 벽3
            data.map[hight - 3, width - 2] = true;
            data.map[hight - 3, width - 3] = true;
            data.map[hight - 3, width - 4] = true;
            data.map[hight - 3, width - 5] = true;
            data.map[hight - 3, width - 6] = true;
            data.map[hight - 3, width - 7] = true;
            data.map[hight - 3, width - 8] = true;
            data.map[hight - 3, width - 9] = true;
            data.map[hight - 3, width - 10] = true;
            data.map[hight - 3, width - 11] = true;
            data.map[hight - 3, width - 12] = true;
            data.map[hight - 3, width - 13] = true;
            data.map[hight - 3, width - 14] = true;
            data.map[hight - 3, width - 15] = true;
            #endregion

            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=           좀비피하기!            =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine("    계속하려면 아무키나 누르세요    ");
            Console.ReadKey();
        }

        static void End()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");

            if (getZombieCount() > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=           ZOMBIE WIN!            =");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=           PLAYER WIN!            =");
                Console.ResetColor();
            }
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        static void mapInit(bool[,] map, int width, int hight, int invenHight)
        {
            for (int i = 0; i < width; i++)
            {
                map[0, i] = true;
                map[hight - 1, i] = true;
                map[hight + invenHight - 1, i] = true;
            }

            for (int i = 0; i < hight + invenHight; i++)
            {
                map[i, 0] = true;
                map[i, width - 1] = true;
            }
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (!data.map[y, x])
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("#");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("P : 1번  R : 2번  S : 3번 ");
            Console.WriteLine("P : 1범위  R : 2범위  S : 2범위 + 다수공격(2)");
            Console.WriteLine("범위란 자신의 위치에 인접한 거리");

        }
        static void PrintItem()
        {
            for (int i = 0; i < data.items.Length; i++)
            {
                if (!data.items[i].Equals(null))
                {
                    Weapon weapon = data.items[i].weapon;
                    Console.SetCursorPosition(data.items[i].point.x, data.items[i].point.y);
                    switch (weapon)
                    {
                        case Weapon.PISTOL:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("P");
                            Console.ResetColor();
                            break;
                        case Weapon.RIFLE:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("R");
                            Console.ResetColor();
                            break;
                        case Weapon.SHOTGUN:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("S");
                            Console.ResetColor();
                            break;
                    }
                }
            }
        }

        static void PrintInventory()
        {
            int itemCount = getInvenCount();

            for (int i = 0; i < itemCount; i++)
            {
                Weapon weapon = data.inventory.weapons[i];
                Console.SetCursorPosition(i + 1, data.inventory.height - 1);
                switch (weapon)
                {
                    case Weapon.PISTOL:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("P");
                        Console.ResetColor();
                        break;
                    case Weapon.RIFLE:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("R");
                        Console.ResetColor();
                        break;
                    case Weapon.SHOTGUN:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("S");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerPos.x, data.playerPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("P");
            Console.ResetColor();
        }

        static void PrintZombies()
        {
            foreach (Zombie zombie in data.zombies)
            {
                if (zombie.point.x != 0 && zombie.point.y != 0)
                {
                    Console.SetCursorPosition(zombie.point.x, zombie.point.y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Z");
                    Console.ResetColor();
                }
            }
        }

        static double CalcEuclid(int diffX, int diifY)
        {

            return Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diifY, 2));
        }

        static int CalcManhattan(int diffX, int diffY)
        {
            return diffX + diffY;
        }

        // 유클리드 거리 기준으로 유저 추격
        static void Chase()
        {
            for (int i = 0; i < data.zombies.Length; i++)
            {

                double[] moveCase = new double[4];
                int move = 0;

                int zombieX = data.zombies[i].point.x;
                int zombieY = data.zombies[i].point.y;

                if (zombieX == 0 && zombieY == 0)
                {
                    continue;
                }
                int playerX = data.playerPos.x;
                int playerY = data.playerPos.y;

                double up = !isWall(zombieX, zombieY - 1) ? CalcEuclid((zombieX - playerX), (zombieY - 1 - playerY)) : data.map.Length;
                double down = !isWall(zombieX, zombieY + 1) ? CalcEuclid((zombieX - playerX), (zombieY + 1 - playerY)) : data.map.Length;
                double left = !isWall(zombieX - 1, zombieY) ? CalcEuclid((zombieX - 1 - playerX), (zombieY - playerY)) : data.map.Length;
                double right = !isWall(zombieX + 1, zombieY) ? CalcEuclid((zombieX + 1 - playerX), (zombieY - playerY)) : data.map.Length;


                moveCase[0] = data.zombies[i].pass[zombieY - 1, zombieX] ? data.map.Length : up;
                moveCase[1] = data.zombies[i].pass[zombieY + 1, zombieX] ? data.map.Length : down;
                moveCase[2] = data.zombies[i].pass[zombieY, zombieX - 1] ? data.map.Length : left;
                moveCase[3] = data.zombies[i].pass[zombieY, zombieX + 1] ? data.map.Length : right;

                for (int j = 1; j < 4; j++)
                {
                    if (moveCase[move] > moveCase[j])
                    {
                        move = j;
                    }
                }

                if (moveCase[move] == data.map.Length)
                {
                    continue;
                }

                switch (move)
                {
                    case 0:
                        data.zombies[i].point = new Point() { x = zombieX, y = zombieY - 1 };
                        break;
                    case 1:
                        data.zombies[i].point = new Point() { x = zombieX, y = zombieY + 1 };
                        break;
                    case 2:
                        data.zombies[i].point = new Point() { x = zombieX - 1, y = zombieY };
                        break;
                    case 3:
                        data.zombies[i].point = new Point() { x = zombieX + 1, y = zombieY };
                        break;
                }
                data.zombies[i].pass[data.zombies[i].point.y, data.zombies[i].point.x] = true;
            }
        }

        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;

            }
        }

        static void attack()
        {
            //맨해튼 거리 기준 차이
            int sight = 0;
            int zombieCount = 0;
            switch (data.inputKey)
            {

                // 권총 
                case ConsoleKey.D1:
                    if (isHaveItem(Weapon.PISTOL, true))
                    {
                        sight = 1;
                        zombieCount = 1;
                    }
                    break;

                // 소총
                case ConsoleKey.D2:
                    if (isHaveItem(Weapon.RIFLE, true))
                    {
                        sight = 2;
                        zombieCount = 1;
                    }
                    break;

                //샷건
                case ConsoleKey.D3:
                    if (isHaveItem(Weapon.SHOTGUN, true))
                    {
                        sight = 2;
                        zombieCount = 2;
                    }
                    break;
            }

            if (sight != 0)
            {
                for (int i = 0; i < data.zombies.Length; i++)
                {

                    if (zombieCount == 0)
                    {
                        break;
                    }

                    if (shoot(sight, data.zombies[i].point))
                    {
                        data.zombies[i].point.x = 0;
                        data.zombies[i].point.y = 0;
                        zombieCount--;

                    }

                }
            }
        }

        static bool isWall(int width, int height)
        {
            return data.map[height, width];
        }
        static bool shoot(int sight, Point zombiePos)
        {
            int playerX = data.playerPos.x;
            int playerY = data.playerPos.y;

            return (CalcManhattan(Math.Abs(zombiePos.x - playerX), Math.Abs(zombiePos.y - playerY)) <= sight * 2);
        }

        static void MoveUp()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y - 1 };
            if (!data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveDown()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y + 1 };
            if (!data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveLeft()
        {
            Point next = new Point() { x = data.playerPos.x - 1, y = data.playerPos.y };
            if (!data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void MoveRight()
        {
            Point next = new Point() { x = data.playerPos.x + 1, y = data.playerPos.y };
            if (!data.map[next.y, next.x])
            {
                data.playerPos = next;
            }
        }

        static void CheckGameClear()
        {
            if (getZombieCount() == 0)
            {
                data.running = false;
                return;
            }
            foreach (Zombie zombie in data.zombies)
            {
                if (CalcEuclid(data.playerPos.x - zombie.point.x, data.playerPos.y - zombie.point.y) <= 1.5 || getZombieCount() == 0)
                {
                    data.running = false;
                    break;
                }
            }

        }

        static int getZombieCount()
        {
            int count = 0;

            foreach (Zombie zombiePos in data.zombies)
            {
                if (zombiePos.point.x != 0)
                {
                    count++;
                }
            }
            return count;
        }

        static int getInvenCount()
        {
            int count = 0;

            if (data.inventory.weapons == null)
            {
                return 0;
            }
            else
            {
                for (int i = 0; i < data.inventory.weapons.Length; i++)
                {
                    if (data.inventory.weapons[i] != Weapon.INIT)
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        static bool isHaveItem(Weapon weapon, bool use = false)
        {
            for (int i = 0; i < data.inventory.weapons.Length; i++)
            {
                Weapon target = data.inventory.weapons[i];

                if (target == weapon)
                {
                    if (use)
                    {
                        data.inventory.weapons[i] = Weapon.INIT;
                    }
                    return true;
                }
            }
            return false;
        }
        static void InventoryArrange()
        {
            Weapon[] temp = new Weapon[10];
            int count = 0;
            for (int i = 0; i < data.inventory.weapons.Length; i++)
            {
                if (data.inventory.weapons[i] != Weapon.INIT)
                {
                    temp[count++] = data.inventory.weapons[i];
                }
            }
            data.inventory.weapons = temp;
        }

        static void GetItem()
        {
            for (int i = 0; i < data.items.Length; i++)
            {
                Item item = data.items[i];
                if (!item.Equals(null))
                {
                    if (item.point.x == data.playerPos.x && item.point.y == data.playerPos.y && item.weapon != Weapon.INIT)
                    {
                        data.inventory.weapons[getInvenCount()] = item.weapon;
                        data.items[i].weapon = Weapon.INIT;
                        break;
                    }
                }
            }
        }

        static void Rander()
        {
            Console.Clear();
            PrintMap();
            PrintItem();
            PrintInventory();
            PrintPlayer();
            PrintZombies();

        }

        static void Input()
        {
            bool check = false;

            do
            {
                data.inputKey = Console.ReadKey(true).Key;

                if (((int)data.inputKey >= 49 && (int)data.inputKey <= 51) || ((int)data.inputKey >= 37 && (int)data.inputKey <= 40))
                {

                    if ((int)data.inputKey >= 49 && (int)data.inputKey <= 51)
                    {
                        if (isHaveItem((Weapon)(data.inputKey - 48)))
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

        static void Update()
        {
            attack();
            CheckGameClear();
            Move();
            Chase();
            GetItem();
            InventoryArrange();
        }

        static void Main(string[] args) 
        {
            GameSystem gameSystem = new GameSystem();
            gameSystem.Start();
            
            while(gameSystem.GetRunning())
            {
                gameSystem.Render();
                gameSystem.Input();
                gameSystem.Update();
            }
            //Start();

            //while (data.running)
            //{
            //    Rander();
            //    Input();
            //    Update();
            //    Thread.Sleep(2);
            //}

            //End();
        }
    }
}
