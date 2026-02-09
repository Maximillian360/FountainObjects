namespace FountainObjects;

public class InputHandler
{
    public PlayerCommand GetCommand(string input)
    {
        while (true)
        {
            PlayerCommand? command = ParseCommand(input);
            if (command != null) return command.Value;
            Console.WriteLine("Input not matched with any recognizable command!");
            continue;
        }
    }
    public PlayerCommand GetAttack(string input)
    {
        while (true)
        {
            PlayerCommand? command = ParseAttack(input);
            if (command != null) return command.Value;
            Console.WriteLine("Input not matched with any recognizable attack!");
            continue;
        }
    }
    
    private PlayerCommand ParseCommand(string input)
    {
        return input switch
        {
            "north" or "w" => new PlayerCommand(ActionType.Move, new Position(-1, 0)),
            "south" or "s" => new PlayerCommand(ActionType.Move, new Position(1, 0)),
            "east" or "d" => new PlayerCommand(ActionType.Move, new Position(0, 1)),
            "west" or "a" => new PlayerCommand(ActionType.Move, new Position(0, -1)),
            "interact" or "i" => new PlayerCommand(ActionType.Interact, new Position(0, 0)),
            "sense" or "h" => new PlayerCommand(ActionType.Sense, new Position(0, 0)),
            "attack" or "v" => new PlayerCommand(ActionType.Attack, new Position(0, 0)),
            _ => new PlayerCommand(ActionType.None, new Position(0,0))
        };
    }

    private PlayerCommand ParseAttack(string input)
    {
        return input switch
        {
            "north" or "w" => new PlayerCommand(ActionType.Attack, new Position(-1, 0)),
            "south" or "s" => new PlayerCommand(ActionType.Attack, new Position(1, 0)),
            "east" or "d" => new PlayerCommand(ActionType.Attack, new Position(0, 1)),
            "west" or "a" => new PlayerCommand(ActionType.Attack, new Position(0, -1)),
        };
    }

    public Difficulty ParseDifficulty(string input)
    {
        return input switch
        {
            "1" or "easy" => Difficulty.Easy,
            "2" or "normal" => Difficulty.Normal,
            "3" or "hard" => Difficulty.Hard,
            "4" or "expert" => Difficulty.Expert,
            _ => Difficulty.Normal
        };
    }
}