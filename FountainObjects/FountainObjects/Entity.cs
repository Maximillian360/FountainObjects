namespace FountainObjects;

public abstract class Entity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Type Type { get; private set; }
    public int MaxHealth { get; private set; }
    public int Health  { get; private set; }
    public Position Position { get; private set; }
    public Position Offset { get; private set; }
    private static int Counter { get; set; } = 1;

    public Entity (string name, Type type, int maxHealth , Position position, Position offset)
    {
        Id = Counter;
        Name = name;
        Type = type;
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Position = position;
        Offset = offset;
        Counter++;
    }
    
}


public enum Type
{
    Player,
    Maelstorm,
    Amarok
}
