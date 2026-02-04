namespace FountainObjects;

public record struct PlayerCommand(
    ActionType Action,
    Position Position);