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
        char padding = ' ';
        Console.WriteLine($"Map size:  {Map.WorldMapCols}x{Map.WorldMapRows} ");
        Player? player = Map.GetEntityById(1) as Player;
        Console.WriteLine($"X: {player.Position.X} Y: {player.Position.Y}");
        
        for (int i = 0; i < Map.WorldMapCols; i++)
        {
            for (int j = 0; j < Map.WorldMapRows; j++)
            {
                Entity? entity = Map.GetEntityByTile(new Position(i, j));
                Console.Write(entity == null ? "| |".PadRight(4, padding) : $"|{entity.Glyph}|".PadRight(4, padding));
            }
            Console.WriteLine();
        }
    }
}