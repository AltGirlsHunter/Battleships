using System;
using System.Collections.Generic;


namespace Battleships.Classes
{
    public class Player
    {
        public Board playerBoard;
        public Board enemyBoard;

        public List<Ship> ships;
        public int turn = 0;

        public void CreateShips(List<(bool isvertical, int x, int y, Ship.ShipTypes type)> r)
        {

            ships = new List<Ship>();

            foreach (var item in r)
            {
                ships.Add(new Ship(item.isvertical, item.x, item.y, playerBoard, item.type));
            }
        }

        public void Shoot(int x, int y)
        {
            this.enemyBoard.SetCell(x, y, Board.CellState.missed);
            foreach (var ship in ships)
            {
                if(ship.IsPartOfShip(x, y))
                {
                    this.enemyBoard.SetCell(x, y, Board.CellState.hit);
                    ship.Hit(this.enemyBoard);
                    break;
                }
            }
        }
    }
}
