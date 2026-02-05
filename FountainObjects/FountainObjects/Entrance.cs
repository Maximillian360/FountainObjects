namespace FountainObjects;

public class Entrance : Tile, IInteractable
{
    private bool _isExitActive = false;

    public Entrance()
    {
        EnteredMessage = "You see light in this room coming from outside the cavern. This is the entrance.";
    }
    
    public void ExitActivated()
    {
        _isExitActive = !_isExitActive;
    }

    public void Interact(Player player)
    {
        if (_isExitActive == false)
        {
            Console.WriteLine("Cannot exit yet, the Fountain of Objects needs to be activated first!");
        }
        Console.Write("Exitted...");
    }
    
    // public bool OnExit()
    // {
    //     if (_isExitActive == false)
    //     {
    //         Console.WriteLine("Cannot exit yet, the Fountain of Objects needs to be activated first!");
    //         return false;
    //     }
    //     return true;
    // }
}