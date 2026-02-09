using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;

namespace FountainObjects;

public class Player : Entity
{
    public bool WinState { get; set; } = false;
    public List<Arrow> Arrows { get; set; }
    private int ArrowLimit { get; init; }
    public Player(string name, char glyph) : base(name: name, type: Type.Player, maxHealth: 1)
    {
        Glyph  = glyph;
        ArrowLimit = 5;
        Arrows = new List<Arrow>();
        MakeArrows(ArrowLimit);

    }

    private void MakeArrows(int arrowLimit)
    {
        for (int i = 0; i < ArrowLimit; i++)
        {
            Arrows.Add(new Arrow(damage: 1, offset: new Position(0, 0)));
        }
    }
    
    
    public void TakePlayerInput(Map map)
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Type W or North, D or East, S or South, A or West to move. ");
            Console.WriteLine("I or Interact, H for Sense, and V or Attack.");
            string? input = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty!");
                continue;
            }
            InputHandler inputHandler = new InputHandler();
            PlayerCommand playerCommand = inputHandler.GetCommand(input);
            
            switch (playerCommand.Action)
            {
                case ActionType.Move:
                    Move(map, playerCommand.Position);
                    break;
                case ActionType.Interact:
                    Interact(map, Position);
                    break;
                case ActionType.Sense:
                    Sense(map, Position);
                    break;
                case ActionType.Attack:
                    if (Arrows.Count <= 0)
                    {
                        Console.WriteLine("No arrows left!");
                        continue;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Type W or North, D or East, S or South, A or West to shoot. ");
                    string? attackInput = Console.ReadLine()?.Trim().ToLower();
                    if (string.IsNullOrWhiteSpace(attackInput))
                    {
                        Console.WriteLine("Attack direction cannot be empty!");
                        continue;
                    }
                    PlayerCommand attackCommand = inputHandler.GetAttack(attackInput);
                    Attack(map, new Position (attackCommand.Position.X + Position.X, attackCommand.Position.Y + Position.Y));
                    break;
                default:
                    Console.WriteLine($"{playerCommand.Action} is unknown");
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
        Tile? tile = map.GetTile(position);
        if (tile == null)
        {
            Console.WriteLine("Tile not found!");
            return;
        }
        Entity? entity = tile.Entity;
        if (entity == null)
        {
            Console.WriteLine("Arrow hit no entity!");
            Arrows.RemoveAt(0);
            return;
        }
        entity.TakeDamage();
        map.WorldMap[position.X, position.Y].CheckEntityDead(map);
        Arrows.RemoveAt(0);
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
