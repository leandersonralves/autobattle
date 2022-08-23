using System;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameSystem gameSystem = new GameSystem();
            gameSystem.StartNewGame(7, 7);
        }
    }
}
