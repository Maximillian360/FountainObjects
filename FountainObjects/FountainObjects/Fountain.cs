namespace FountainObjects;

public class Fountain : Tile, IInteractable
{
    private static readonly string FountainInactiveMessage = "You hear water dripping in this room. The Fountain of Objects is here!";
    private static readonly string FountainActiveMessage = "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";
    public string FountainMessage { get; private set; } = FountainInactiveMessage;
    
    
    public override void OnTileEntered()
    {
        Console.WriteLine(FountainMessage);
    }

    public void Interact(Player player, Entrance entrance)
    {
        FountainMessage = (FountainMessage == FountainInactiveMessage) ?  FountainInactiveMessage : FountainActiveMessage;
        entrance.ExitActivated();

    }
    
}