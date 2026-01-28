using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace FountainObjects;

public class Map
{
    public int WorldMapCols { get; private set; }
    public int WorldMapRows { get; private set; }
    public Tile[,] WorldMap { get; private set; }

    public Map(int worldMapCols, int worldMapRows)
    {
        WorldMapCols = worldMapCols;
        WorldMapRows = worldMapRows;
        WorldMap = new Tile[WorldMapRows, WorldMapCols];
    }
    
    public bool IsPositionInside (Position position) => position.X >= 0 &&  position.X < WorldMapRows && position.Y >= 0 && position.Y < WorldMapCols;

    public Tile? GetTile(Position position) => IsPositionInside(position) ? WorldMap[position.X, position.Y] : null;

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

    public void TryUpdatePosition(Position position, Entity? entity)
    {
        if (entity == null)
        {
            Console.WriteLine("No entity found, cannot update position.");
            return;
        }
        if (GetTile(new Position(position.X, position.Y)) == null)
        {
            Console.WriteLine("No tile found, cannot update position.");
            return;
        }
        
        Position newPosition = new Position(position.X + entity.Position.X, position.Y + entity.Position.Y);

        if (!IsPositionInside(position) || !IsPositionInside(newPosition))
        {
            Console.WriteLine("Position is out of bounds, cannot update position.");
            return;
        }

        if (GetEntityByTile(newPosition) != null)
        {
            Console.WriteLine("Tile is occupied!");
            return;
        }
        WorldMap[newPosition.X, newPosition.Y].Entity = entity;
        WorldMap[entity.Position.X, entity.Position.Y].Entity = null;
        WorldMap[newPosition.X, newPosition.Y].Entity.PositionUpdate(newPosition);
    }

    public void SenseNearbyTiles(Position position)
    {
        for (int i = 0; i < 3 ; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1) continue;
                if (!IsPositionInside(new Position(position.X + i - 1, position.Y + j - 1))) continue;
                Tile? checkTile = GetTile(new Position(position.X - i - 1, position.Y - j - 1));
                if (checkTile == null) continue;
                
            }
        }
    }

    public void GenerateMap()
    {
        var player = new Player("Player");
        WorldMap[0, 0] = new Entrance();
        TryPlaceEntity(new Position(0, 0), player);
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                if (i == 0 && j == 0) continue;

                if (i == WorldMapRows - 1 && j == WorldMapCols - 1)
                {
                    WorldMap[i, j] = new Fountain();
                }
                
                if (GetTile(new Position(i, j)) == null)
                {
                    WorldMap[i, j] = new Ground();
                }
                
                // if (GetEntityByTile(new Position(i, j)) != null)
                // {
                //     continue;
                // }

                
            }
        }
    }


}