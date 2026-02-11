using Type = FountainObjects.Core.Enums.Type;

namespace FountainObjects.Core.Entities;

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

    public Entity (string name, Type type, int maxHealth)
    {
        Id = Counter;
        Name = name;
        Type = type;
        Glyph = ' ';
        MaxHealth = maxHealth;
        Health = MaxHealth;
        Position = new Position(0, 0);
        Counter++;
        EntitySenseMessage = $"Nothing unusual...";
    }

    public void PositionUpdate(Position newPosition, Tile tile)
    {
        Position = newPosition;
        string message = tile.OnTileEntered(this);
        Console.WriteLine(message);
    }
    
    protected void Move(Map map, Position position)
    {
        if (position != null)
        {
            map.TryUpdatePosition(position, this);
        }
        
    }
    
    public void TakeDamage()
    {
        Health -= 1;
    }

    
}
