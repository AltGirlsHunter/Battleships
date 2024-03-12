using System;
using System.Collections.Generic;


namespace Battleships.Classes
{
    public class GameState
    {
        public static int c = 0;
        public Player player1;
        public Player player2;
        public static GameState instance;

        public GameState()
        {
            instance = this;
        }
        internal void initialize()
        {
            player1 = new Player();
            player2 = new Player();
            player1.playerBoard = new Board();
            player2.playerBoard = new Board();
            player1.enemyBoard = player2.playerBoard;
            player2.enemyBoard = player1.playerBoard;
        }
        public enum WhoWon
        {
            Noone,
            Player1,
            Player2,
        }
        public WhoWon DidSomeoneWin()
        {
            if (!HasAnyShips(player1.playerBoard)) return WhoWon.Player2;
            if (!HasAnyShips(player2.playerBoard)) return WhoWon.Player1;
            return WhoWon.Noone;

        }
        public bool HasAnyShips(Board board)
        {
            bool hasAnyShips = false;
            for(int x = 0;x<board.getArrayLength(0); x++ )
            {
                for (int y = 0; y < board.getArrayLength(1); y++)
                {
                    hasAnyShips |= board.IsCellEqualTo(x,y, Board.CellState.ship);
                }
            }
            return hasAnyShips;

        }
    }
}
