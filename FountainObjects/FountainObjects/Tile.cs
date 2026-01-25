namespace FountainObjects;

public abstract class Tile
{
    public Entity? Entity { get; set; }
    public string TileMessage { get; set; } = "Base Tile Message";
    
    public Tile (Entity? entity = null)
    {
        Entity = entity;
    }

    public virtual void OnTileEntered()
    {
        Console.WriteLine("Tile Entered Base Method.");
    }
}