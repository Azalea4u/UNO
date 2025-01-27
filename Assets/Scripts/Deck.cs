using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> drawPile = new List<GameObject>();
	[SerializeField]
	private List<GameObject> discardPile = new List<GameObject>();
	[SerializeField] GameObject cardPrefab;

	public void InitializeDeck()
	{
		discardPile.Clear();

		string[] colors = { "red", "blue", "green", "yellow" };
		foreach (var color in colors)
		{
			for (int i = 0; i <= 9; i++)
			{
				GameObject ACard = (Instantiate(cardPrefab));
				Card TheCard = ACard.GetComponent<Card>();
				TheCard.Number = i;
				TheCard.Color = color;
				TheCard.Type = CardType.Number;
				TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/" + color + "/UNO" + color + i);
				drawPile.Add(ACard);
			}

			for (int i = 1; i <= 9; i++)
			{
				GameObject ACard = (Instantiate(cardPrefab));
				Card TheCard = ACard.GetComponent<Card>();
				TheCard.Number = i;
				TheCard.Color = color;
				TheCard.Type = CardType.Number;
				TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/" + color + "/UNO" + color + i);
				drawPile.Add(ACard);
			}

			for (int i = 0; i < 2; i++)
			{
				GameObject ACard = (Instantiate(cardPrefab));
				Card TheCard = ACard.GetComponent<Card>();
				TheCard.Number = -1;
				TheCard.Color = color;
				TheCard.Type = CardType.Skip;
				TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/" + color + "/UNO" + color + "Skip");
				drawPile.Add(ACard);

				ACard = (Instantiate(cardPrefab));
				TheCard = ACard.GetComponent<Card>();
				TheCard.Number = -2;
				TheCard.Color = color;
				TheCard.Type = CardType.Reverse;
				TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/" + color + "/UNO" + color + "Reverse");
				drawPile.Add(ACard);

				ACard = (Instantiate(cardPrefab));
				TheCard = ACard.GetComponent<Card>();
				TheCard.Number = -3;
				TheCard.Color = color;
				TheCard.Type = CardType.Draw2;
				TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/" + color + "/UNO" + color + "+2");
				drawPile.Add(ACard);
			}
		}

		for (int i = 0; i < 4; i++)
		{
			GameObject ACard = (Instantiate(cardPrefab));
			Card TheCard = ACard.GetComponent<Card>();
			TheCard.Number = -4;
			TheCard.Color = "Black";
			TheCard.Type = CardType.Wild;
			TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/other/UNOwild");
			drawPile.Add(ACard);

			ACard = (Instantiate(cardPrefab));
			TheCard = ACard.GetComponent<Card>();
			TheCard.Number = -5;
			TheCard.Color = "Black";
			TheCard.Type = CardType.Wild4;
			TheCard.Image.sprite = Resources.Load<Sprite>("Images/Uno_Cards/other/UNOwild+4");
			drawPile.Add(ACard);
		}

		Shuffle();
		DiscardFirstCard();
	}

	private void DiscardFirstCard()
	{
		GameObject card = DrawCard();
		DiscardCard(card);
	}

	public void Shuffle()
	{
		drawPile = drawPile.OrderBy(c => Random.value).ToList();
	}

	public GameObject DrawCard()
	{
		if (drawPile.Count == 0) ReshuffleDiscardPile();
		if (drawPile.Count == 0) return null;

		GameObject drawnCard = drawPile[0];
		drawPile.RemoveAt(0);
		return drawnCard;
	}

	public void DiscardCard(GameObject card)
	{
		discardPile.Add(card);
	}

	private void ReshuffleDiscardPile()
	{
		drawPile = discardPile;
		discardPile = new List<GameObject>();
		Shuffle();
	}

	public GameObject getTopOfDiscard()
	{
		return discardPile.Last();
	}
}
