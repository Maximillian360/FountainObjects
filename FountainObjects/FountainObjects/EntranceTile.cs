namespace FountainObjects;

public class EntranceTile : Tile
{
    public override void OnTileEntered()
    {
        Console.WriteLine($"OnTileEtnered");
    }
}