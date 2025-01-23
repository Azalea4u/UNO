using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<Card> drawPile = new List<Card>();
    private List<Card> discardPile = new List<Card>();

    private void Start()
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
                drawPile.Add(new Card(i, color, CardType.Number));
            }

			for (int i = 1; i <= 9; i++)
			{
				drawPile.Add(new Card(i, color, CardType.Number));
			}

            for(int i = 0; i < 2; i++)
            {
                drawPile.Add(new Card(-1, color, CardType.Skip));
                drawPile.Add(new Card(-2, color, CardType.Reverse));
                drawPile.Add(new Card(-3, color, CardType.Draw2));
            }
		}

        for (int i = 0; i < 4; i++)
        {
			drawPile.Add(new Card(-4, "Black", CardType.Wild));
			drawPile.Add(new Card(-5, "Black", CardType.Wild4));
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

    public Card getTopOfDiscard()
    {
        return drawPile.Last();
    }
}
