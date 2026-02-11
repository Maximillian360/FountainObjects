namespace FountainObjects.Core.Tiles;

public class Ground : Tile
{
    public Ground() : base()
    {
        if (Entity != null)
        {
            SenseMessage = Entity.EntitySenseMessage;
        }
    }
}