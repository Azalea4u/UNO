using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public string Name;
	private List<Card> Hand;

	private void Start()
	{
		Hand = new List<Card>();
	}

	public Card PlayCard(int arrayIndex)
	{
		Card card = Hand[arrayIndex];
		Hand.RemoveAt(arrayIndex);
		return card;
	}

	public void DrawCard(Deck deck)
	{
		Hand.Add(deck.DrawCard());
	}

	public void AddCardToHand(Card card)
	{
		Hand.Add(card);
	}

	public int GetHandCount()
	{
		return Hand.Count;
	}
}
