namespace FountainObjects;

public class Maelstorm : Entity
{
    public Maelstorm() : base(name: "Maelstorm", type: Type.Maelstorm, maxHealth: 1)
    {
        EntitySenseMessage = "You hear the growling and groaning of a Maelstorm nearby.";
    }
}