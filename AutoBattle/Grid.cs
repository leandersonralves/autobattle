using System;
using System.Collections.Generic;
using AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLength;
        public int yLength;

        public int AmountCount { get; private set; }

        public Grid(int lines, int columns)
        {
            AmountCount = lines * columns;
            xLength = columns;
            yLength = lines;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < lines; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (i * columns + j));
                    grids.Add(newBox);
                    //Console.Write($"Index {newBox.Index}\n");
                    //Console.Write($"indexlist {grids.IndexOf(newBox)}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void DrawBattlefield()
        {
            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    GridBox g = grids[i * xLength + j];
                    if (g.ocupied)
                    {
                        Console.Write($"[X]\t");
                    }
                    else
                    {
                        Console.Write("[ ]\t");
                        //Console.Write($"[{g.xIndex} , {g.yIndex}]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
    }
}
