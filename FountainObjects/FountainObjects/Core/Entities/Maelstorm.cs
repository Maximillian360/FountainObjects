namespace FountainObjects.Core.Entities;

public class Maelstorm : Entity
{
    public Maelstorm() : base(name: "Maelstorm", type: Core.Enums.Type.Maelstorm, maxHealth: 1)
    {
        EntitySenseMessage = "You hear the growling and groaning of a Maelstorm nearby.";
    }
}