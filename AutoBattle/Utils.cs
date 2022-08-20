using System;

namespace AutoBattle
{
    class Utils
    {
        public static int GetRandomInt(int min, int max)
        {
            var rand = new Random();
            int index = rand.Next(min, max);
            return index;
        }
    }
}
