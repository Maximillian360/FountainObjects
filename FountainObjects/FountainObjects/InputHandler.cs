namespace FountainObjects;

public class InputHandler
{
    public PlayerCommand GetCommand(string input)
    {
        while (true)
        {
            PlayerCommand? command = ParseCommand(input);
            if (command == null)
            {
                Console.WriteLine("Input not matched with any recognizable command!");
                continue;
            }
            return command.Value;
        }
    }


    private PlayerCommand ParseCommand(string input)
    {
        return input switch
        {
            "north" or "n" => new PlayerCommand(ActionType.Move, new Position(-1, 0)),
            "south" or "s" => new PlayerCommand(ActionType.Move, new Position(1, 0)),
            "east" or "e" => new PlayerCommand(ActionType.Move, new Position(0, 1)),
            "west" or "w" => new PlayerCommand(ActionType.Move, new Position(0, -1)),
            "interact" or "i" => new PlayerCommand(ActionType.Interact, new Position(0, 0)),
            "attack" or "a" => new PlayerCommand(ActionType.Attack, new Position(0, 0)),
            _ => new PlayerCommand(ActionType.None, new Position(0,0))
        };
    }
}