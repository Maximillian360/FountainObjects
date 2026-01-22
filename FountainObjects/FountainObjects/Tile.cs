namespace FountainObjects;

public class Tile
{
    public Entity? Entity { get; set; }
    
    public Tile (Entity? entity = null)
    {
        Entity = entity;
    }
}

public enum Category
{
    Ground,
    Pit
}