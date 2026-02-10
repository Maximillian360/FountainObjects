using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

namespace FountainObjects;

public class Map
{
    public int WorldMapRows { get; private set; }
    public int WorldMapCols { get; private set; }
    public Tile[,] WorldMap { get; private set; }
    
    public int PitLimit { get; private set; }
    public int PitCounter { get; private set; }
    public int AmarokLimit { get; private set; }
    public int AmarokCounter { get; private set; }
    public int MaelstormLimit { get; private set; }
    public int MaelstormCounter { get; private set; }

    public Map(int worldMapRows, int worldMapCols, int pitLimit, int amarokLimit, int maelstormLimit)
    {
        WorldMapRows = worldMapRows;
        WorldMapCols = worldMapCols;
        WorldMap = new Tile[WorldMapRows, WorldMapCols];
        PitLimit = pitLimit;
        PitCounter = 0;
        AmarokCounter = 0;
        AmarokLimit = amarokLimit;
        MaelstormLimit = maelstormLimit;
        MaelstormCounter = 0;
        GenerateMap();
    }
    
    public bool IsPositionInside (Position position) => position.X >= 0 && position.X < WorldMapRows && position.Y >= 0 && position.Y < WorldMapCols;

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
    
    public Entity? GetEntityById(int id)
    {
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                Tile? tile = GetTile(new Position(i, j));
                if (tile == null) continue;
                if (tile.Entity == null) continue;
                if (tile.Entity.Id != id) continue;
                return tile.Entity;
            }
        }
        return null;
    }

    public void TryPlaceEntity(Position position, Entity? entity)
    {
        if (!IsPositionInside(position))
        {
            Console.WriteLine("Tile outside of bounds, cannot place entity.");
            return;
        }
        
        if (entity == null)
        {
            Console.WriteLine("No entity found, cannot place entity.");
            return;
        }

        if (GetTile(new Position(position.X, position.Y)) == null)
        {
            Console.WriteLine("Tile does not exist, cannot place entity.");
            return;
        }

        if (GetTile(new Position(position.X, position.Y)) is Pit)
        {
            Console.WriteLine("Cannot place entity on pit");
            return;
        }

        if (GetEntityByTile(new Position(position.X, position.Y)) != null)
        {
            Console.WriteLine("Tile is occupied!");
            return;
        }
        
        WorldMap[position.X, position.Y].Entity = entity;
        WorldMap[position.X, position.Y].Entity.PositionUpdate(position, GetTile(position));
    }

    public void TryUpdatePosition(Position position, Entity? entity)
    {
        Position newPosition = new Position(position.X + entity.Position.X, position.Y + entity.Position.Y);
        if (entity == null)
        {
            Console.WriteLine("No entity found, cannot update position.");
            return;
        }
        
        if (!IsPositionInside(newPosition))
        {
            Console.WriteLine("Position is out of bounds, cannot update position.");
            return;
        }
        
        if (GetTile(new Position(newPosition.X, newPosition.Y)) == null)
        {
            Console.WriteLine("No tile found, cannot update position.");
            return;
        }

        Entity? entityInPosition = GetEntityByTile(newPosition);
        
        if (entityInPosition?.Type == Type.Amarok)
        {
            Console.WriteLine($"Tile is occupied by: {entityInPosition.Type}!");
            entity.TakeDamage();
            Console.WriteLine($"An {entityInPosition.Type} has attacked you!");
            return;
        }
        
        if (entityInPosition?.Type == Type.Maelstorm)
        {
            Console.WriteLine($"Tile is occupied by {entityInPosition.Type}!");
            Position[] positions = MaelstormPush(entityInPosition, newPosition);
            ActualMove(positions[0], entity);
            ActualMove(positions[1], entityInPosition);
            return;
        }
        ActualMove(newPosition, entity);
    }

    public Position[] MaelstormPush(Entity maelstorm, Position newPosition)
    {
        int playerPushX = newPosition.X + 2;
        int playerPushY = newPosition.Y - 1;
        int maelstormPushX = maelstorm.Position.X - 2;
        int maelstormPushY = maelstorm.Position.Y + 1;
        Position playerPushOffset = new Position(
            Math.Clamp(playerPushX, 0, WorldMapRows - 1),
            Math.Clamp(playerPushY, 0, WorldMapCols - 1));
        Position maelstormPushOffset = new Position(
            Math.Clamp(maelstormPushX, 0, WorldMapRows - 1), 
            Math.Clamp(maelstormPushY, 0, WorldMapCols - 1));
        return new [] { playerPushOffset, maelstormPushOffset };
    }

    public void ActualMove(Position newPosition, Entity? entity)
    {
        WorldMap[newPosition.X, newPosition.Y].Entity = entity;
        if (!entity.Position.Equals(newPosition))
        {
            WorldMap[entity.Position.X, entity.Position.Y].Entity = null;
        }
        WorldMap[newPosition.X, newPosition.Y].Entity.PositionUpdate(newPosition, GetTile(newPosition));
    }

    public void SetExit()
    {
        if (WorldMap[0, 0] is Entrance entrance)
        {
            entrance.ToggleExitState();
        }
        Console.WriteLine("Exit toggled.");
    }
    

    public void SenseNearbyTiles(Position position)
    {
        Tile?[] nearbyTiles = GetNearbyTiles(position);
        foreach (Tile? nearbyTile in nearbyTiles)
        {
            if (nearbyTile == null) continue;
            string senseMessage = nearbyTile.GetSenseMessage();
            if (senseMessage != "Nothing unusual.")
            {
                Console.WriteLine(senseMessage);
            }
        }
    }

    public Tile?[] GetNearbyTiles(Position position)
    {
        Tile?[] nearbyTiles = new Tile[8];
        int index = 0;
        for (int i = 0; i < 3 ; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (i == 1 && j == 1) continue;
                if (!IsPositionInside(new Position(position.X + i - 1, position.Y + j - 1)))
                {
                    nearbyTiles[index] = null;
                }
                Tile? checkTile = GetTile(new Position(position.X + i - 1, position.Y + j - 1));
                nearbyTiles[index] = checkTile;
                index++;
            }
        }
        return nearbyTiles;
    }
    
    public void GenerateMap()
    {
        var player = new Player("Player", '@');
        WorldMap[0, 0] = GenerateEntrance(0,0);
        TryPlaceEntity(new Position(0, 0), player);
        Random random = new Random();
        for (int i = 0; i < WorldMapRows; i++)
        {
            for (int j = 0; j < WorldMapCols; j++)
            {
                if (GetEntityByTile(new Position(i, j)) != null) continue;
                
                if (i == WorldMapRows - 1 && j == WorldMapCols - 1)
                {
                    WorldMap[i, j] = GenerateFountain(i, j);
                    continue;
                }
                
                if (GetTile(new Position(i, j)) == null && PitCounter < PitLimit && random.Next(0, 5) == 0)
                {
                    WorldMap[i, j] = GeneratePit(i, j);
                    PitCounter++;
                    continue;
                }
        
                if (GetTile(new Position(i, j)) == null)
                {
                    WorldMap[i, j] = GenerateGround(i, j);
                }

                if (GetEntityByTile(new Position(i, j)) == null && AmarokCounter < AmarokLimit &&
                    random.Next(0, 7) == 0)
                {
                    TryPlaceEntity(new Position(i, j), new Amarok());
                    AmarokCounter++;
                }

                if (GetEntityByTile(new Position(i, j)) == null && MaelstormCounter < MaelstormLimit && random.Next(0, 10) == 0)
                {
                    TryPlaceEntity(new Position(i, j), new Maelstorm());
                    MaelstormCounter++;
                }
            }
        }
    }

    private Fountain GenerateFountain(int i, int j) => new Fountain();
    
    private Ground GenerateGround(int i, int j) => new Ground();
    
    private Pit GeneratePit(int i, int j) => new Pit();
    
    private Entrance GenerateEntrance(int i, int j) => new Entrance();

    public static Map CreateEasyMap() => new Map(worldMapRows: 4, worldMapCols: 4, pitLimit: 1, amarokLimit: 1, maelstormLimit: 1);
    public static Map CreateNormalMap() => new Map(worldMapRows:6, worldMapCols: 6, pitLimit: 2, amarokLimit: 2, maelstormLimit: 1);
    public static Map CreateHardMap() => new Map(worldMapRows:8, worldMapCols: 8, pitLimit: 4, amarokLimit: 3, maelstormLimit: 2);
    public static Map CreateExpertMap() => new Map(worldMapRows: 10, worldMapCols: 10, pitLimit: 5, amarokLimit: 4, maelstormLimit: 3);
}