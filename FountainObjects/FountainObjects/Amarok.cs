namespace FountainObjects;

public class Amarok : Entity
{
    // public string SenseMessage { get; set; }
    public Amarok() : base(name: "Amarok", type: Type.Amarok, maxHealth: 1)
    {
        EntitySenseMessage = "You can smell the rotten stench of an amarok in a nearby room.";
    }
    
    
}