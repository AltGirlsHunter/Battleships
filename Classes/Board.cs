using System;
using System.Collections.Generic;


namespace Battleships.Classes
{
    public class Board
    {
        private CellState[,] boardState;

        public Board()
        {
            boardState = new CellState[10, 10];
        }
        public CellState GetCell(int x, CharacterValues y)
        {
            return GetCell(x, (int)y);
        }

        public CellState GetCell(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < getArrayLength(0) && y < getArrayLength(1)) return boardState[x, y];
            return default;
        }
        public void SetCell(int x, int y, CellState state)
        {
            if (x >= boardState.GetLength(0) || x < 0 || y >= boardState.GetLength(1) || y < 0) return;
            boardState[x, y] = state;
        }
        public bool IsCellEqualTo(int x, CharacterValues y, CellState value)
        {
            return IsCellEqualTo(x, (int)y, value);
        }
        public bool IsCellEqualTo(int x, int y, CellState value)
        {
            return GetCell(x, y) == value;
        }
        public void Fill(int x1, int y1, int x2, int y2, CellState value)
        {
            OrderedFill(x1>x2?x2:x1, y1 > y2 ? y2 : y1, x1 > x2 ? x2 : x2, y1 > y2 ? y1 : y2, value);
        }
        private void OrderedFill(int x1, int y1, int x2, int y2, CellState value)
        {
            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    SetCell(x, y, value);
                }
            }
        }
        public int getArrayLength(int v)
        {
            return boardState.GetLength(v);
        }

        public enum CharacterValues
        {
            a = 0,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i,
            j
        }
        public enum CellState
        {
            empty = 0,
            missed,
            locked,
            ship,
            hit,
            sunk = 10
        }
        public bool CanShipBePlacedHere(int x, CharacterValues y)
        {
            return CanShipBePlacedHere(x, (int)y);
        }
        public bool CanShipBePlacedHere(int x, int y)
        {

            bool False = x >= 0 && y >= 0 && x < getArrayLength(0) && y < getArrayLength(1);

            for(int _x = x-1; _x<=x+1;_x++)
            {
                for (int _y = y - 1; _y <= y + 1; _y++)
                {
                    var zzzz = this.boardState;
                    False &= !IsCellEqualTo(_x, _y, Board.CellState.ship);
                }
            }
            return False;
        }
        public bool CanShipBePlacedHere(int x, int y, int l, bool isVertical)
        {

            bool False = isInBounds(x,y, l, isVertical);
            for (int i = isVertical?y:x; i<=(isVertical ? y : x)+l;i++)
            {
                var x1 = isVertical ? i : x;
                var y1 = isVertical ? y : i;
                False &= CanShipBePlacedHere(isVertical ? x : i, isVertical ? i : y);
            }
            return False;
        }
        public bool CanThisSpotBeShot(int x, int y)
        {
            bool canItBe = isInBounds(x, y);
            canItBe &= !(IsCellEqualTo(x,y, CellState.missed) || IsCellEqualTo(x,y, CellState.hit));
            return canItBe;
        }

        private bool isInBounds(int x, int y)
        {
            return y >= 0 && y < boardState.GetLength(1) &&  x < boardState.GetLength(0) && x >= 0;

        }
        private bool isInBounds(int x, int y, int l, bool isVertical)
        {
            if (isVertical) return y>=0 && y < boardState.GetLength(1) &&y + l < boardState.GetLength(1);
            return x >= 0 && x < boardState.GetLength(1) && x + l < boardState.GetLength(1);

        }
    }

}
