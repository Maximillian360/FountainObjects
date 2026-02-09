namespace FountainObjects;

public class Arrow
{
    public int Damage { get; set; }
    public Position Offset { get; set; }
    public Arrow(int damage, Position offset)
    {
        Damage = 1;
        Offset = offset;
    }
}