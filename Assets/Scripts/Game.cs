using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField]
	public Player[] players;
	[HideInInspector]
	public Player currentPlayer;
	private int currentPlayerIndex;
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
		}

		chooseFirstPlayer();
	}

	public void nextTurn()
	{
		if (reversed)
		{
			if (currentPlayerIndex > 0) currentPlayerIndex--;
			else currentPlayerIndex = players.Length;
		}
		else
		{
			if (currentPlayerIndex < players.Length - 1) currentPlayerIndex++;
			else currentPlayerIndex = 0;
		}

		currentPlayer = players[currentPlayerIndex];
	}

	public void DrawCard()
	{
		currentPlayer.DrawCard(deck);
	}

	public void PlayCard(int arrayIndex)
	{
		Card playedCard = currentPlayer.PlayCard(arrayIndex);

		bool valid = false;

		if (playedCard != null)
		{
			switch (playedCard.Type)
			{
				case (CardType.Number):
					if ((checkColor(playedCard)) || (playedCard.Value == deck.getTopOfDiscard().Value))
					{
						checkForWin();

						valid = true;
						nextTurn();
					}
					break;
				case (CardType.Skip):
					if ((checkColor(playedCard)) || (deck.getTopOfDiscard().Type == CardType.Skip))
					{
						checkForWin();

						valid = true;
						nextTurn();
						nextTurn();
					} 
					break;
				case (CardType.Reverse):
					if ((checkColor(playedCard)) || (deck.getTopOfDiscard().Type == CardType.Reverse))
					{
						checkForWin();

						valid = true;
						reversed = true;
						nextTurn();
					}
					break;
				case (CardType.Draw2):
					if ((checkColor(playedCard)) || (deck.getTopOfDiscard().Type == CardType.Draw2))
					{
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
					checkForWin();

					ChooseColor();
					nextTurn();
					break;
				case (CardType.Wild4):
					checkForWin();

					valid = true;
					ChooseColor();
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

	private bool checkColor(Card playedCard)
	{
		if(playedCard == null) return false;
		if (playedCard.Color.Equals(deck.getTopOfDiscard().Color)) return true;
		return false;
	}

	private void ChooseColor()
	{

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
