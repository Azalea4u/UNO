using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardType type;
    [SerializeField] private string color;
    [SerializeField] private int number;

    public CardType Type
    {
        get => type;
        set => type = value;
    }

    public string Color
    {
        get => color;
        set => color = value;
    }

    public int Number
    {
        get => number;
        set => number = value;
    }

    public Card(int value, string color, CardType type)
    {
        this.number = value;
        this.color = color;
        this.type = type;
    }

    public override string ToString()
    {
        return $"{color} {number} {type}";
    }
}
