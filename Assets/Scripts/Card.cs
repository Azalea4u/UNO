using UnityEngine;

public class Card
{
	public int Value { get; private set; }
    public string Color { get; private set; }
    public CardType Type {get; private set; }

    public Card(int value, string color, CardType type)
    {
        Value = value;
        Color = color;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Color} {Value} {Type}";
    }
}
