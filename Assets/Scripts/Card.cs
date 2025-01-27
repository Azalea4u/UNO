using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Card : MonoBehaviour
{
    [SerializeField] private CardType type;
    [SerializeField] private string color;
    [SerializeField] private int number;
    [SerializeField] private Image image;


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

    public Image Image
    {
        get => image;
        set => image = value;
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
