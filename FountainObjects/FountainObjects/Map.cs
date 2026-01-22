using System.Reflection.Metadata;

namespace FountainObjects;

public class Map
{
    public int MapHeight { get; private set; }
    public int MapWidth { get; private set; }
    public Tile[,] WorldMap { get; private set; }

    public Map(int mapHeight, int mapWidth)
    {
        MapHeight = mapHeight;
        MapWidth = mapWidth;
        WorldMap = new Tile[MapWidth, MapHeight];
    }
    
    public bool IsPointInside (Position position) => position.X >= 0 &&  position.X < MapWidth && position.Y >= 0 && position.Y < MapHeight;

    public Tile? GetTile(Position position) => IsPointInside(position) ? WorldMap[position.X, position.Y] : null;

    public Entity? GetEntityByTile(Position position)
    {
        if (GetTile(new Position(position.X, position.Y)) == null)
        {
            return null;
        }

        if (WorldMap[position.X, position.Y].Entity == null)
        {
            return null;
        }
        return WorldMap[position.X, position.Y].Entity;
    }

    public void TryPlaceEntity(Position position,  Entity? entity)
    {
        if (GetTile(new Position(position.X, position.Y)) == null)
        {
            Console.WriteLine("No tile found, cannot place entity.");
            return;
        }
        if (entity == null)
        {
            Console.WriteLine("No entity found, cannot place entity.");
            return;
        }

        if (GetEntityByTile(new Position(position.X, position.Y)) != null)
        {
            Console.WriteLine("Tile is occupied!");
            return;
        }
        WorldMap[position.X, position.Y].Entity = entity;
    }

    public void GenerateMap()
    {
        var player = new Player("Player");
        TryPlaceEntity(new Position(0, 0), player);
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                if (i == 0 && j == 0) continue;
                
                if (GetTile(new Position(i, j)) == null)
                {
                    WorldMap[i, j] = new Tile();
                }
                
                // if (GetEntityByTile(new Position(i, j)) != null)
                // {
                //     continue;
                // }

                
            }
        }
    }
}