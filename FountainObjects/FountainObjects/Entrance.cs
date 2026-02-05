namespace FountainObjects;

public class Entrance : Tile, IInteractable
{
    private bool _isExitActive = false;

    public Entrance()
    {
        EnteredMessage = "You see light in this room coming from outside the cavern. This is the entrance.";
        SenseMessage = "The exit is nearby...";
    }
    
    public void ToggleExitState()
    {
        _isExitActive = !_isExitActive;
    }
    
    public void Interact(Player player)
    {
        if (_isExitActive == false)
        {
            Console.WriteLine("Cannot exit yet, the Fountain of Objects needs to be activated first!");
            return;
        }
        Console.Write("Exiting out of the caves... You won!");
        player.WinState = true;
    }
}