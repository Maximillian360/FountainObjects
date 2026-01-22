namespace FountainObjects;

public class Player : Entity
{
    public Player(string name) : base(name: name, type: Type.Player, maxHealth: 1,
        position: new Position(0,0), offset: new Position(0,0))
    {
        
    }
    
    
}