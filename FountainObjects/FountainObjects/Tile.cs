namespace FountainObjects;

public abstract class Tile
{
    public Entity? Entity { get; set; }
    public string EnteredMessage { get; set; }
    
    public Tile (Entity? entity = null)
    {
        Entity = entity;
    }

    public virtual string OnTileEntered() => EnteredMessage;
}