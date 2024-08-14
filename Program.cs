using System.Data;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using ZombieGame;
using static ConsoleProject1.Program;

namespace ConsoleProject1
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            GameSystem gameSystem = new GameSystem();
            gameSystem.Start();
            
            while (gameSystem.GetRunning())
            {
                gameSystem.Render();
                gameSystem.Input();
                gameSystem.Update();
            }
            gameSystem.End();
        }
    }
}
