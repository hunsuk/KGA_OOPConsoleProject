using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZombieGame.Utility
{
    public class Ui
    {
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=           좀비피하기!            =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine("    계속하려면 아무키나 누르세요    ");
            Console.ReadKey();
            Console.Clear();
        }

        public void End(bool userWin)
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            if (userWin)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=           PLAYER WIN!            =");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=           ZOMBIE WIN!            =");
                Console.ResetColor();
            }
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        public void DrawMap(Map map)
        {
            List<Pixel> paths = map.GetMap();

            for (int y = 0; y < map.GetHight(); y++)
            {
                for (int x = 0; x < map.GetWidth(); x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (paths[x + map.GetWidth() * y].GetIsbeing() == null)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        if (paths[x + map.GetWidth() * y].GetIsbeing().IsExistence())
                        {
                            Console.Write(paths[x + map.GetWidth() * y].GetIsbeing().GetSign());
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("P : 1번  R : 2번  S : 3번 ");
            Console.WriteLine("P : 1범위  R : 2범위  S : 2범위 + 다수공격(2)");
            Console.WriteLine("범위란 자신의 위치에 인접한 거리");
        }
    }
}
