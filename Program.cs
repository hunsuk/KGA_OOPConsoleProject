using System.Data;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using ZombieGame;
using static ConsoleProject1.Program;

namespace ConsoleProject1
{
    internal class Program
    {
     
        

        static int CalcManhattan(int diffX, int diffY)
        {
            return diffX + diffY;
        }

        // 유클리드 거리 기준으로 유저 추격
        /*
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

    */

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
            gameSystem.End();
        }
    }
}
