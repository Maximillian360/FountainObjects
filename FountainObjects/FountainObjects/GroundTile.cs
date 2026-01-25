namespace FountainObjects;

public class GroundTile : Tile
{
    public override void OnTileEntered()
    {
        Console.WriteLine($"OnTileEtnered");
    }
}