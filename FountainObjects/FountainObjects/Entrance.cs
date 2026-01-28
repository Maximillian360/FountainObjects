namespace FountainObjects;

public class Entrance : Tile
{
    private bool _isExitActive = false;
    public string EntranceMessage { get; set; } = "You see light in this room coming from outside the cavern. This is the entrance.";
    
    
    public override void OnTileEntered()
    {
        Console.WriteLine(EntranceMessage);
    }

    public void ExitActivated()
    {
        _isExitActive = !_isExitActive;
    }
    
    public bool OnExit()
    {
        if (_isExitActive == false)
        {
            Console.WriteLine("Cannot exit yet, the Fountain of Objects needs to be activated first!");
            return false;
        }
        return true;
    }
}