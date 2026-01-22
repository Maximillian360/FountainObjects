namespace FountainObjects;

public abstract class Entity
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public Type Type { get; private set; }
    public int MaxHealth { get; private set; }
    public int Health  { get; private set; }
    
}


public enum Type
{
    Player,
    Maelstorm,
    Amarok
}
