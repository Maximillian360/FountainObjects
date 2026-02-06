// See https://aka.ms/new-console-template for more information

using FountainObjects;

Console.WriteLine("Hello, World!");
PlayGame();



static void PlayGame()
{
    GameDifficulty gameDifficulty = new GameDifficulty();
    Map map = gameDifficulty.DifficultySelector(gameDifficulty.Difficulty);
    Player? player = map.GetEntityByTile(new Position(0,0)) as Player;
    Renderer renderer = new Renderer(map);

    while (!player.WinState && player.Health >= 1)
    {
        renderer.RenderWorldMap();
        player.TakePlayerInput(map);
    }

    if (player.Health <= 0)
    {
        Console.WriteLine("You Died!");
    }
    
}




