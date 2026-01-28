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
        Console.WriteLine($"Map size:  {Map.WorldMapRows}x{Map.WorldMapCols} ");
        for (int i = 0; i < Map.WorldMapRows; i++)
        {
            for (int j = 0; j < Map.WorldMapCols; j++)
            {
                Entity? entity = Map.GetEntityByTile(new Position(i, j));
                Console.Write(entity == null ? "| |".PadRight(4, padding) : $"|{entity.Glyph}|".PadRight(4, padding));
            }
            Console.WriteLine();
        }
    }
}