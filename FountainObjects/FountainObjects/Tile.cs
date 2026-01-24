namespace FountainObjects;

public class Tile
{
    public Entity? Entity { get; set; }
    public Category Category { get; init; }
    
    public Tile (Entity? entity = null, Category category = Category.Ground)
    {
        Entity = entity;
        Category = category;
    }
}

public enum Category
{
    Ground,
    Pit
}