using System;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSystem gameSystem = new GameSystem();
            gameSystem.Setup(7, 7);
        }
    }
}
