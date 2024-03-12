using Battleships;
using Battleships.Classes;

//here goes nothing ig


//Initialization

GameState gameState = new GameState();
gameState.initialize();
CLI cli = new CLI();
Console.WriteLine("Welcome to BattleShips!");
Console.WriteLine("Initialization: ");
cli.FakeLoadingBar(4, 20);


Player[] players = {gameState.player1, gameState.player2 };

Player? winner = null;

var l = gameState.DidSomeoneWin();
for (GameState.c = 0; winner == null; GameState.c++)
{
    Console.WriteLine($"Round of {(GameState.WhoWon)(GameState.c % 2)+1}");
    cli.AwaitAffirmation();
    cli.HandlePlayer(players[GameState.c % 2]);

    if (players[GameState.c % 2].turn > 1)
    {
        l = gameState.DidSomeoneWin();
        switch (l)
        {
            case GameState.WhoWon.Player1:
                winner = players[0];
                break;
            case GameState.WhoWon.Player2:
                winner = players[1];
                break;
            default:
                break;
        }
    } 
}
Console.WriteLine($"{l} won!");


