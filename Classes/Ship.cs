


namespace Battleships.Classes
{
    public class Ship
    {
        public int x, y;

        ShipTypes shipType;

        public bool isVertical;
        public Ship(bool isVertical, int x, int y, Board board, ShipTypes shipType)
        {
            this.isVertical = isVertical;
            this.x = x;
            this.y = y;
            if (isVertical)
            {
                board.Fill(x - 1, y-1, x + 1, y + (int) shipType+1, Board.CellState.locked);
                board.Fill(x, y, x, y + (int)shipType, Board.CellState.ship); 
            }
            else
            {
                board.Fill(x - 1, y - 1, x + (int)shipType + 1, y + 1, Board.CellState.locked);
                board.Fill(x, y, x + (int)shipType, y, Board.CellState.ship); 
            }
        }

        public enum ShipTypes
        {
            empty = 0,
            destroyer,
            submarine,
            battleship,
            carrier
        }

        public bool IsPartOfShip(int x, int y)
        {
            if (isVertical) return this.x == x && this.y <= y && this.y+(int)shipType>=y;
            else return this.y == y && this.x >= x && this.x+(int)shipType<=x;
        }

        public void Hit(Board b)
        {
            bool bul = true;
            for(int i = 0; i<(int)shipType; i++)
            {
                bul &= b.IsCellEqualTo(isVertical ? x : x + i, isVertical ? y : y + 1, Board.CellState.hit);
            }
            if(bul)
            for (int i = 0; i <= (int)shipType; i++)
            {
                b.SetCell(isVertical ? x : x + i, isVertical ? y : y + 1, Board.CellState.sunk);
            }
        }
    }
}