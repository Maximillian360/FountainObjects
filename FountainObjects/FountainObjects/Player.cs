using System.Reflection.Metadata.Ecma335;

namespace FountainObjects;

public class Player : Entity
{
    public Player(string name) : base(name: name, type: Type.Player, maxHealth: 1, glyph: '@',
        position: new Position(0,0))
    {
        
    }
    

    public void TakePlayerInput(Map map)
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Type W or North, D or East, S or South, A  West to move. ");
            Console.WriteLine("I or Interact, and V or Attack.");
            string? input = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty!");
                continue;
            }
            InputHandler inputHandler = new InputHandler();
            PlayerCommand playerInput = inputHandler.GetCommand(input);

            switch (playerInput.Action)
            {
                case ActionType.Move:
                    Console.WriteLine($"{playerInput.Action} is moving {playerInput.Position.X}, {playerInput.Position.Y}");
                    Move(map, playerInput.Position);
                    break;
                case ActionType.Interact:
                    Console.WriteLine($"{playerInput.Action} is interacting");
                    break;
                case ActionType.Attack:
                    Console.WriteLine($"{playerInput.Action} is attacking");
                    break;
                default:
                    Console.WriteLine($"{playerInput.Action} is unknown");
                    break;
            }

            break;
        }
    }

    public void Move(Map map, Position position)
    {
        if (position != null)
        {
            map.TryUpdatePosition(position, this);
            return;
        }
        
    }

    public void Interact(Map map, Position position)
    {
        
    }

    public void Attack(Map map, Position position)
    {
        
    }
    
}

public enum ActionType
{
    Move,
    Interact,
    Attack,
    None,
}
