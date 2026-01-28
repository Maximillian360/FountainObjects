// See https://aka.ms/new-console-template for more information

using FountainObjects;

Console.WriteLine("Hello, World!");



static void PlayGame()
{
    Map map = new Map(worldMapCols: 4, worldMapRows: 4);
    Player? player = map.GetEntityByTile(new Position(0,0)) as Player;
    Renderer renderer = new Renderer(map);

    while (true)
    {
        renderer.RenderWorldMap();
        Position position = player.TakePosition();
        map.TryUpdatePosition(position, player);
    }
}