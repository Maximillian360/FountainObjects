namespace FountainObjects;

public class Player : Entity
{
    public Player(string name) : base(name: name, type: Type.Player, maxHealth: 1, glyph: '@',
        position: new Position(0,0))
    {
        
    }

    public Position TakePosition()
    {
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Type North, East, South, or West to move. ");
            string? input = Console.ReadLine()?.ToLower();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty!");
                continue;
            }

            if (!(input == "north" || input == "east" || input == "south" || input == "west"))
            {
                Console.WriteLine($"Unrecognized input!: {input}");
                continue;
            }

            Position position = input switch
            {
                "north" => new Position(-1, 0),
                "south" => new Position(1, 0),
                "east" => new Position(0, 1),
                "west" => new Position(0, -1),
                _ => new Position(0, 0)
            };
            
            return position;
        }
    }
    
    
}
