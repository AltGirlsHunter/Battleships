using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using static Battleships.Classes.Ship;

namespace Battleships.Classes
{
    public class CLI
    {
        private enum Display
        {
            _ = 0,
            x = 1,
            y = 2,
            v = 3,
            s = 4
        }
        public char GetDisplayedChar(int i)
        {
            switch (i)
            {
                case 0:return '_';
                case 1: return '0';
                case 2: return '_';
                case 3: return '@';
                case 4: return 'X';
                case 10: return '#';
                default:
                    return 'v';
            }
        }
        public char GetDisplayedCharForEnemyBoard(int i)
        {
            switch (i)
            {
                case 0: return '_';
                case 1: return '0';
                case 2: return '_';
                case 3: return '_';
                case 4: return 'X';
                case 10: return '#';
                default:
                    return 'v';
            }
        }
        public char GetDisplayedChar(Board.CellState i)
        {
            return GetDisplayedChar((int)i);
        }
        public void AwaitAffirmation()
        {
            Console.Clear();
            String s = "";
            do
            {
                Console.WriteLine("Type <c> to continue");
                 s = Console.ReadLine();
                if (String.IsNullOrEmpty(s)) s = "1";
            } while (s.Trim()[0] != 'c');


        }
        public void HandlePlayer(Player player)
        {
            this.ShowBoards(player);
            if (player.turn == 0)
            {
                AskForShips(player);
            }
            else
            {
                AskAboutShooting(player);
            }
            Console.Clear();
            this.ShowBoards(player);
            Console.WriteLine("Press Enter to continue.");
            Console.ReadKey();
            player.turn++;
        }

        private void AskAboutShooting(Player player)
        {
            bool canBe = true;
            var cords = GetInputPosition();
            do
            {
                canBe = player.enemyBoard.CanThisSpotBeShot(cords.x, cords.y);
            } while (!canBe);
            player.Shoot(cords.x, cords.y);
        }

        public void AddToCreated(bool isvertical, int x, int y, Ship.ShipTypes type, Player player)
            {          
            if(player.ships==null) player.ships = new List<Ship>();

            player.ships.Add(new Ship(isvertical, x, y, player.playerBoard, type));
           
        }
        public void AskForShips(Player player)
        {

            ShowBoards(player);

            RequestShip(0, player);

            ShowBoards(player);
            RequestShip(0, player);

            ShowBoards(player);
            RequestShip(0, player);

            ShowBoards(player);
            RequestShip(1, player);

            ShowBoards(player);
            RequestShip(1, player);

            ShowBoards(player);
            RequestShip(2, player);

            ShowBoards(player);
            RequestShip(2, player);

            ShowBoards(player);
            RequestShip(3, player);

        }

        private void RequestShip(int l, Player player)
        {
            (int x, int y, bool isVertical) input;
            Console.WriteLine($"Set {(Ship.ShipTypes)l+1}");
            bool b = true;
            do
            {

                input = GetShipInput(player, (Ship.ShipTypes)l); ;
                b = player.playerBoard.CanShipBePlacedHere(input.x, input.y, l, input.isVertical);
                if (b) break;
                Console.WriteLine("You can't place a ship here");
            } while (true) ;
            AddToCreated(input.isVertical, input.x, input.y, (Ship.ShipTypes)l, player);
        }

        public void ShowBoards(Player player) 
        {
                        Console.Clear();
            Console.WriteLine($" Round of {(GameState.WhoWon)(GameState.c % 2) + 1}");
            int offset = 4;
            Board b = player.playerBoard;
            Board e = player.enemyBoard;
            for(int x = 0;x < b.getArrayLength(0); x++) {

                Console.SetCursorPosition(offset + 2*x, offset - 1);
                Console.Write(x+1 + " ");
                for (int y = 0; y < b.getArrayLength(1); y++)
                {
                    Console.SetCursorPosition(offset - 2, offset + y);
                    Console.Write((Board.CharacterValues) y+ " ");
                    Console.SetCursorPosition(offset + 2*x, offset + y);
                    Console.Write(GetDisplayedChar(b.GetCell(x,y)) + " ");

                    Console.SetCursorPosition(2*offset + 20 + 2*x, offset + y);
                    Console.Write(GetDisplayedCharForEnemyBoard((int) e.GetCell(x, y)) + " ");
                }
            }
            Console.WriteLine();
            var top = Console.GetCursorPosition().Top;
            Console.WriteLine("                                                                                        ");
            Console.WriteLine("                                                                                        ");
            Console.WriteLine("                                                                                        ");
            Console.SetCursorPosition(0, top);
        }
        public (int x, int y, bool isVertical) GetShipInput(Player player, ShipTypes type)
        {
            bool isVertical;
            (int x, int y) _;
            do {
                 _ = GetInputPosition();
                Console.WriteLine("Is the ship placed vertically? (y => yes) ");
            string? r = Console.ReadLine();
            if (r == null) continue;
            isVertical = r.Trim().ToLower()[0] == 'y';

                break;
            } while (true);
            return (_.x, _.y, isVertical);
        }
        public (int x, int y) GetInputPosition()
        {
            int x, y;
            string? input;
            do
            {
                Console.WriteLine("Place inputs <letter><number>");
                input = Console.ReadLine();
                
                if (input == null) continue;
                if (input.Length < 2) continue;

                y = input.Trim().ToLower()[0] - 'a';
                if(!(y>=0&&y<10)) continue;

                x = input[1] - '1';
                if (input.Length > 2 && input[2] == '0') x = 9;
                if (!(x >= 0 && x < 10)) continue;

                break;

            } while (true);
            return (x, y);
        }

        public void FakeLoadingBar(int t, int l)
        {
            var item = Console.GetCursorPosition();
            for (int i = 0; i < l; i++) Console.Write("-");

            for (int i = 0; i < l; i++)
            {
                Console.SetCursorPosition(item.Left + i, item.Top);
                Console.Write("#");
                Console.SetCursorPosition(item.Left + l, item.Top);
                Console.Write((int)(((float)i / l) * 100) + "%");
                Thread.Sleep((t * 1000) / l);
            }
            Console.SetCursorPosition(item.Left + l, item.Top);
            Console.Write(100 + "%");
            Console.WriteLine();
        }
    }
}
