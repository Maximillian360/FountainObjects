namespace FountainObjects;

public class Renderer
{
    Map Map { get; init; }

    public Renderer(Map map)
    {
        Map = map;
    }
    
    public void RenderWorldMap()
    {
        // Console.Clear();
        char padding = ' ';
        Console.WriteLine($"Map size:  {Map.WorldMapCols}x{Map.WorldMapRows} ");
        Player? player = Map.GetEntityById(1) as Player;
        Console.WriteLine($"X: {player.Position.X} Y: {player.Position.Y}");
        // string[] messages = new string[Map.WorldMapCols * Map.WorldMapRows];
        for (int i = 0; i < Map.WorldMapCols; i++)
        {
            for (int j = 0; j < Map.WorldMapRows; j++)
            {
                Entity? entity = Map.GetEntityByTile(new Position(i, j));
                Console.Write(entity == null ? "| |".PadRight(4, padding) : $"|{entity.Glyph}|".PadRight(4, padding));
                // messages[j] = Map.WorldMap[i, j].OnTileEntered();
            }
            Console.WriteLine();
        }
        
        // foreach (string message in messages) Console.WriteLine(message);
    }
}