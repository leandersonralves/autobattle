﻿using AutoBattle.Characters;

namespace AutoBattle.Types
{
    public struct GridBox
    {
        public int xIndex;
        public int yIndex;
        public bool ocupied;
        public int Index { get; private set; }

        public GridBox(int x, int y, bool ocupied, int index)
        {
            xIndex = x;
            yIndex = y;
            this.ocupied = ocupied;
            this.Index = index;
        }
    }
}
