namespace FountainObjects;

public class Pit : Tile
{
    public int Damage { get; init; }

    public Pit()
    {
        Damage = 1;
        EnteredMessage = "You have fallen to your death!";
        SenseMessage = "You can feel a draft of air pushing through nearby";
    }

    public override string OnTileEntered(Player player)
    {
        player.TakeDamage();
        return base.OnTileEntered(player);
    }
}