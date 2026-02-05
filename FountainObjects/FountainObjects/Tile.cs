namespace FountainObjects;

public abstract class Tile : IInteractable
{
    public Entity? Entity { get; set; }
    public string EnteredMessage { get; set; }
    public string InteractMessage { get; set; } = "Doesn't do anything.";
    
    public Tile (Entity? entity = null)
    {
        Entity = entity;
    }

    public void Interact(Player player)
    {
        Console.WriteLine($"{player.Position.X}, {player.Position.Y}: {InteractMessage}");
    }

    public virtual string OnTileEntered() => EnteredMessage;
}