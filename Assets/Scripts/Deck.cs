using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();

    public Deck()
    {
        InitializeDeck();
        Shuffle();
    }

    private void InitializeDeck()
    {
        string[] colors = { "Red", "Blue", "Green", "Yellow" };
        foreach (var color in colors)
        {
            for (int i = 0; i <= 9; i++)
            {
                drawPile.Add(new Card(i, color));
            }
        }
    }

    public void Shuffle()
    {
        drawPile = drawPile.OrderBy(c => Random.value).ToList();
    }

    public Card DrawCard()
    {
        if (drawPile.Count == 0) ReshuffleDiscardPile();
        if (drawPile.Count == 0) return null;

        Card drawnCard = drawPile[0];
        drawPile.RemoveAt(0);
        return drawnCard;
    }

    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
    }

    private void ReshuffleDiscardPile()
    {
        drawPile = discardPile;
        discardPile = new List<Card>();
        Shuffle();
    }
}
