namespace FountainObjects;

public abstract class Entity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Type Type { get; private set; }
    public int MaxHealth { get; private set; }
    public int Health  { get; protected set; }
    public char Glyph { get; init; }
    public Position Position { get; private set; }
    private static int Counter { get; set; } = 1;
    public string EntitySenseMessage { get; set; }

    public Entity (string name, Type type, int maxHealth , Position position, char glyph = '@')
    {
        Id = Counter;
        Name = name;
        Type = type;
        Glyph = glyph;
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Position = position;
        Counter++;
        EntitySenseMessage = $"Entity {Type} is nearby...";
    }

    public void PositionUpdate(Position newPosition, Tile? tile)
    {
        Position = newPosition;
        string message = tile.OnTileEntered();
        Console.WriteLine(message);
    }
    
    public void TakeDamage(Map map, Position position)
    {
        Health -= 1;
    }

    
}


public enum Type
{
    Player,
    Maelstorm,
    Amarok
}
