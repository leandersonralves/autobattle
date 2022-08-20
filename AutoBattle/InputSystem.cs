using System;

namespace AutoBattle
{
    public class InputSystem
    {
        public static int ReadInt (string messageInvalid)
        {
            return ReadInt(int.MinValue, int.MaxValue, messageInvalid);
        }

        public static int ReadInt (int min, int max, string messageInvalid)
        {
            bool validInput = false;
            int value = 0;

            while(!validInput)
            {
                string inputReaded = Console.ReadLine();
                validInput = int.TryParse(inputReaded, out value);
                validInput = validInput && value >= min && value <= max;
                if (!validInput) // show message warning if Input isn't a number and isn't between min/max.
                {
                    Console.WriteLine(messageInvalid);
                }
            }

            return value;
        }
    }
}
