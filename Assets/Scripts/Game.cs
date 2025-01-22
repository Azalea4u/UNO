using UnityEngine;

public class Game : MonoBehaviour
{
	[SerializeField]
	public Player[] players;
	public Player currentPlayer;
	private int currentPlayerIndex;
	[SerializeField]
	public Deck deck;

	public void StartGame()
	{
		foreach (Player player in players)
		{
			for (int i = 0; i < 7; i++)
			{
				player.DrawCard(deck);
			}
		}

		chooseFirstPlayer();
	}

	public void nextTurn()
	{
		if (currentPlayerIndex < players.Length) currentPlayerIndex++;
		else currentPlayerIndex = 0;

		currentPlayer = players[currentPlayerIndex];
	}

	public void DrawCard()
	{
		currentPlayer.DrawCard(deck);
	}

	public void PlayCard(int arrayIndex)
	{
		Card playedCard = currentPlayer.PlayCard(arrayIndex);
		deck.DiscardCard(playedCard);
	}

	private void chooseFirstPlayer()
	{
		if(Random.value > 0.5)
		{
			currentPlayer = players[0];
		}
		else
		{
			currentPlayer = players[1];
		}
	}
}
