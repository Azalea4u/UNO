using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public string Name;
	[SerializeField]
	public List<GameObject> Hand;
	[SerializeField]
	public GameObject PlayerHandUI;

	public void InitializePlayer()
	{
		Hand = new List<GameObject>();
	}

	public GameObject PlayCard(GameObject card)
	{
		Hand.Remove(card);
		card.gameObject.SetActive(false);
		UpdateHandUI();
		return card;
	}

	public void DrawCard(Deck deck)
	{
		Hand.Add(deck.DrawCard());
		UpdateHandUI();
	}

	public void AddCardToHand(GameObject card)
	{
		Hand.Add(card);
		UpdateHandUI();
	}

	public int GetHandCount()
	{
		return Hand.Count;
	}

	public void UpdateHandUI()
	{
		foreach (GameObject Card in Hand)
		{
			Card.transform.SetParent(PlayerHandUI.transform);
		}
	}
}
