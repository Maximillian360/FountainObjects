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

    public void GenerateMap()
    {
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                if (GetTile(new Position(i, j)) == null)
                {
                    WorldMap[i, j] = new Tile();
                }
            }
        }
    }
}