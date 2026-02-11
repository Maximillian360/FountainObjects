
namespace FountainObjects.Core.Entities;


public class Amarok : Entity
{
    public Amarok() : base(name: "Amarok", type: Core.Enums.Type.Amarok, maxHealth: 1)
    {
        EntitySenseMessage = "You can smell the rotten stench of an amarok in a nearby room.";
    }
    
    
}