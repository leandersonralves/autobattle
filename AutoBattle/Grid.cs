using System;
using AutoBattle.Characters;

namespace AutoBattle
{
    public class Grid
    {
        public int XLength { get; private set; }

        public int YLength { get; private set; }

        private Character[] characters;

        public Grid(int lines, int columns, Character[] characters)
        {
            XLength = columns;
            YLength = lines;
            this.characters = characters;
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            for (int y = 0; y < YLength; y++)
            {
                for (int x = 0; x < XLength; x++)
                {
                    bool isOcuppied = false;
                    for (int j = 0; j < characters.Length; j++)
                    {
                        if (characters[j].currentBox.xIndex == x && characters[j].currentBox.yIndex == y)
                        {
                            Console.Write($"[{ characters[j].PlayerIndex }]\t");
                            isOcuppied = true;
                            break;
                        }
                    }
                    if (!isOcuppied)
                    {
                        Console.Write("[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }
    }
}
