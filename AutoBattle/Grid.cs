using System;
using System.Collections.Generic;
using AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;

        public int AmountCount { get; private set; }

        public Grid(int lines, int columns)
        {
            AmountCount = lines * columns;
            xLenght = columns;
            yLength = lines;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < lines; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (lines * i + j));
                    grids.Add(newBox);
                    //Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            for (int i = 0; i < xLenght; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    if (grids[i * xLenght + j].ocupied)
                    {
                        Console.Write($"[{grids[i * xLenght + j].Index}X]\t");
                    }
                    else
                    {
                        Console.Write($"[{grids[i * xLenght + j].Index}]\t");
                        //Console.Write("[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
}
