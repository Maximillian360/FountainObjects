using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;

namespace FountainObjects;

public class Player : Entity
{
    public bool WinState { get; set; } = false;
    public Arrow[]  Arrows { get; set; }
    private int ArrowLimit { get; init; }
    public Player(string name, char glyph) : base(name: name, type: Type.Player, maxHealth: 1)
    {
        Glyph  = glyph;
        ArrowLimit = 5;
        Arrows = new Arrow[5];
        MakeArrows(ArrowLimit);

    }

    private void MakeArrows(int arrowLimit)
    {
        for (int i = 0; i < ArrowLimit; i++)
        {
            Arrows[i] = new Arrow(damage: 1, offset: new Position(0,0));
        }
    }
    
    
    public void TakePlayerInput(Map map)
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Type W or North, D or East, S or South, A  West to move. ");
            Console.WriteLine("I or Interact, H for Sense, and V or Attack.");
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
                    Move(map, playerInput.Position);
                    break;
                case ActionType.Interact:
                    Interact(map, Position);
                    break;
                case ActionType.Sense:
                    Sense(map, Position);
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

    
    private void Interact(Map map, Position position)
    {
        if (map.GetTile(position) is IInteractable interactable)
        {
            interactable.Interact(this);
            if (position == new Position(map.WorldMapRows - 1, map.WorldMapCols - 1))
            {
                map.SetExit();
            }
            
            return;
        }
        Console.WriteLine($"Tile in Position: {position} and Type: {map.GetTile(position)} is not interactable!");
    }

    private void Sense(Map map, Position position)
    {
        map.SenseNearbyTiles(position);
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
    Sense,
    None,
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Expert
}
