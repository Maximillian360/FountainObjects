// See https://aka.ms/new-console-template for more information

using FountainObjects;

Console.WriteLine("Hello, World!");
PlayGame();



static void PlayGame()
{
    GameDifficulty gameDifficulty = new GameDifficulty();
    Map map = DifficultySelector(gameDifficulty.Difficulty);
    // Map map = new Map(worldMapRows: 4, worldMapCols: 4);
    Player? player = map.GetEntityByTile(new Position(0,0)) as Player;
    Renderer renderer = new Renderer(map);

    while (!player.WinState)
    {
        renderer.RenderWorldMap();
        player.TakePlayerInput(map);
        // break;
        // Position position = player.TakePlayerInput();
        // map.TryUpdatePosition(position, player);
    }
}

static Map DifficultySelector(Difficulty gameDifficulty)
{
    return gameDifficulty switch
    {
        Difficulty.Easy => Map.CreateEasyMap(),
        Difficulty.Normal => Map.CreateNormalMap(),
        Difficulty.Hard => Map.CreateHardMap(),
        Difficulty.Expert => Map.CreateExpertMap(),
        _ => Map.CreateNormalMap()
    };
}


