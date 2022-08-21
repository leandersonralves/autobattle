using System;

namespace AutoBattle
{
    class Utils
    {
        private static readonly Random _random = new Random();

        public static int GetRandomInt(int min, int max)
        {
            int index = _random.Next(min, max);
            return index;
        }
    }
}
