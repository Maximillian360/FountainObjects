using System.Reflection.Metadata.Ecma335;

namespace FountainObjects;

public class Player : Entity
{
    public Player(string name) : base(name: name, type: Type.Player, maxHealth: 1, glyph: '@',
        position: new Position(0,0))
    {
        
    }

    // public override void PositionUpdate(Position newPosition, Tile? tile)
    // {
    //     base.PositionUpdate(newPosition, tile);
    //     string message = tile.OnTileEntered();
    //     Console.WriteLine(message);
    //     Console.WriteLine("awsdjlasjdlkasjkdlasldjlasdas");
    // }

    public void TakePlayerInput(Map map)
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Type N or North, E or East, S or South, W  West to move. ");
            Console.WriteLine("I or Interact, and A or Attack.");
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
                    Console.WriteLine($"{playerInput.Action} is moving");
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

            // if (!(input == "north" || input == "east" || input == "south" || input == "west"))
            // {
            //     Console.WriteLine($"Unrecognized input!: {input}");
            //     continue;
            // }

            // Position position = input switch
            // {
            //     "north" => new Position(-1, 0),
            //     "south" => new Position(1, 0),
            //     "east" => new Position(0, 1),
            //     "west" => new Position(0, -1),
            //     _ => new Position(0, 0)
            // };
            //
            // return position;
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
    
}

public enum ActionType
{
    Move,
    Interact,
    Attack,
    None,
}
