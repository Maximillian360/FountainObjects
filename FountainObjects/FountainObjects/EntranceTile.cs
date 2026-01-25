namespace FountainObjects;

public class EntranceTile : Tile
{
    private bool _isExitActive = false;

    public EntranceTile() : base()
    {
        TileMessage = "You see light in this room coming from outside the cavern. This is the entrance.";
    }
    
    public override void OnTileEntered()
    {
        Console.WriteLine(TileMessage);
    }
    
    public bool OnExit()
    {
        if (!_isExitActive)
        {
            Console.WriteLine("Cannot exit yet, the Fountain of Objects needs to be activated first!");
            return false;
        }
        _isExitActive = true;
        return true;
    }
}