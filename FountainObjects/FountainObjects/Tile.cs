namespace FountainObjects;

public abstract class Tile : IInteractable
{
    public Entity? Entity { get; set; }
    public string EnteredMessage { get; set; }
    public string InteractMessage { get; set; } = "Doesn't do anything.";
    public string SenseMessage { get; set; } = "Nothing unusual.";
    
    public Tile (Entity? entity = null)
    {
        Entity = entity;
    }

    public void Interact(Player player)
    {
        Console.WriteLine($"{InteractMessage}");
    }

    public string GetSenseMessage()
    {
        if (Entity != null)
        {
            return Entity.EntitySenseMessage;
        }
        return SenseMessage;
    }

    public virtual string OnTileEntered() => EnteredMessage;
}