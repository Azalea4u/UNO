using UnityEngine;

public class Card : MonoBehaviour
{
    public int Value { get; private set; }
    public string Color { get; private set; }

    public Card(int value, string color)
    {
        Value = value;
        Color = color;
    }

    public override string ToString()
    {
        return $"{Color} {Value}";
    }
}
