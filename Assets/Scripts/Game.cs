using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField]
	public Player[] players;
	[HideInInspector]
	public Player currentPlayer;
	public int currentPlayerIndex;
	[SerializeField]
	public Deck deck;


	private bool reversed;
	[HideInInspector]
	public bool gameOver;

	[HideInInspector]
	public Player winner;

	public void StartGame()
	{
		deck.InitializeDeck();

		foreach (Player player in players)
		{
			player.InitializePlayer();

			for (int i = 0; i < 7; i++)
			{
				player.DrawCard(deck);
			}

			player.UpdateHandUI();
		}

		chooseFirstPlayer();
		foreach (Player player in players)
		{
			if (player != currentPlayer)
			{
				foreach (GameObject ACard in player.Hand)
				{
					ACard.GetComponent<DragDrop>().enabled = false;
				}
			} else
			{
				foreach (GameObject ACard in currentPlayer.Hand)
				{
					ACard.GetComponent<DragDrop>().enabled = true;
				}
			}
		}
	}

	public void nextTurn()
	{
		if (reversed)
		{
			if (currentPlayerIndex > 0) currentPlayerIndex--;
			else currentPlayerIndex = players.Length - 1;
		}
		else
		{
			if (currentPlayerIndex < players.Length - 1) currentPlayerIndex++;
			else currentPlayerIndex = 0;
		}

		foreach(GameObject ACard in currentPlayer.Hand)
		{
			ACard.GetComponent<DragDrop>().enabled = false;
		}
		currentPlayer = players[currentPlayerIndex];
		foreach (GameObject ACard in currentPlayer.Hand)
		{
			ACard.GetComponent<DragDrop>().enabled = true;
		}
	}

	public void DrawCard()
	{
		currentPlayer.DrawCard(deck);
	}

	public void PlayCard(GameObject playedCard)
	{
		bool valid = false;

		if (playedCard != null)
		{
			switch (playedCard.GetComponent<Card>().Type)
			{
				case (CardType.Number):
					if ((checkColor(playedCard)) || (playedCard.GetComponent<Card>().Number == deck.getTopOfDiscard().GetComponent<Card>().Number))
					{
						currentPlayer.PlayCard(playedCard);
						checkForWin();

						valid = true;
						nextTurn();
					}
					break;
				case (CardType.Skip):
					if ((checkColor(playedCard)) || (deck.getTopOfDiscard().GetComponent<Card>().Type == CardType.Skip))
					{
						currentPlayer.PlayCard(playedCard);
						checkForWin();

						valid = true;
						nextTurn();
						nextTurn();
					} 
					break;
				case (CardType.Reverse):
					if ((checkColor(playedCard)) || (deck.getTopOfDiscard().GetComponent<Card>().Type == CardType.Reverse))
					{
						currentPlayer.PlayCard(playedCard);
						checkForWin();

						valid = true;
						reversed = !reversed;

						if (players.Length > 2)
						{
							nextTurn();
						}
					}
					break;
				case (CardType.Draw2):
					if ((checkColor(playedCard)) || (deck.getTopOfDiscard().GetComponent<Card>().Type == CardType.Draw2))
					{
						currentPlayer.PlayCard(playedCard);
						checkForWin();

						valid = true;
						nextTurn();
						currentPlayer.DrawCard(deck);
						currentPlayer.DrawCard(deck);
						nextTurn();
					}
					break;
				case (CardType.Wild):
					valid = true;
					currentPlayer.PlayCard(playedCard);
					checkForWin();

					nextTurn();
					break;
				case (CardType.Wild4):
					currentPlayer.PlayCard(playedCard);
					checkForWin();

					valid = true;
					nextTurn();
					currentPlayer.DrawCard(deck);
					currentPlayer.DrawCard(deck);
					currentPlayer.DrawCard(deck);
					currentPlayer.DrawCard(deck);
					nextTurn();
					break;
				default:
					print("What?!, this should not be happening");
					break;
			}
		}

		if (valid) deck.DiscardCard(playedCard);
		else currentPlayer.AddCardToHand(playedCard);
	}

	private void checkForWin()
	{
		if(currentPlayer.GetHandCount() == 0)
		{
			winner = currentPlayer;
			gameOver = true;
		}
	}

	private bool checkColor(GameObject playedCard)
	{
		if(playedCard == null) return false;
		if (playedCard.GetComponent<Card>().Color.Equals(deck.getTopOfDiscard().GetComponent<Card>().Color)) return true;
		return false;
	}

	public void DrawCardFromDeck()
	{
		currentPlayer.DrawCard(deck);
		nextTurn();
	}

	private void chooseFirstPlayer()
	{
		currentPlayer = players[Random.Range(0, players.Length)];
	}
}
