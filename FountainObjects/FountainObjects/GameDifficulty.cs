namespace FountainObjects;

public class GameDifficulty
{
    public Difficulty Difficulty { get; private set; }
    
    public GameDifficulty()
    {
        TakeDifficultyInput();
    }
    public void TakeDifficultyInput()
    {
        while (true)
        {
            Console.WriteLine("Difficulty Selector");
            Console.WriteLine("Press 1 for Easy, 2 for Medium, 3 for  Hard, 4 for Expert: ");
            string? difficultyInput = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(difficultyInput))
            {
                Console.WriteLine("Difficulty Input cannot be empty!");
                continue;
            }
            InputHandler inputHandler = new InputHandler();
            Difficulty = inputHandler.ParseDifficulty(difficultyInput);
            break;
        }
    }
}