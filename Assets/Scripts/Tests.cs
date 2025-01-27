using NUnit.Framework;
using UnityEngine;

public class GameTests
{
	[Test]
	public void TestPlayersStartWithSevenCards()
	{
		// Arrange
		var gameObject = new GameObject();
		var game = gameObject.AddComponent<Game>();

		game.players = new Player[2];
		for (int i = 0; i < 2; i++)
		{
			var playerObject = new GameObject($"Player{i}");
			game.players[i] = playerObject.AddComponent<Player>();
		}

		var deckObject = new GameObject("Deck");
		game.deck = deckObject.AddComponent<Deck>();
		game.deck.InitializeDeck();

		// Act
		game.StartGame();

		// Assert
		foreach (var player in game.players)
		{
			Assert.AreEqual(7, player.GetHandCount(), $"Player {player.name} does not have 7 cards.");
		}
	}

	[Test]
	public void TestPlayValidNumberCard()
	{
		// Arrange
		var gameObject = new GameObject();
		var game = gameObject.AddComponent<Game>();

		var deckObject = new GameObject("Deck");
		game.deck = deckObject.AddComponent<Deck>();
		game.deck.InitializeDeck();

		var playerObject = new GameObject("Player");
		var player = playerObject.AddComponent<Player>();
		game.players = new[] { player };
		game.currentPlayer = player;

		var topCard = new GameObject("TopCard").AddComponent<Card>();
		topCard.Type = CardType.Number;
		topCard.Number = 5;
		topCard.Color = "Red";

		var validCard = new GameObject("ValidCard").AddComponent<Card>();
		validCard.Type = CardType.Number;
		validCard.Number = 5; // Matches by number
		validCard.Color = "Blue"; // Different color

		game.deck.DiscardCard(topCard.gameObject); // Set top of discard
		player.AddCardToHand(validCard.gameObject);

		// Act
		game.PlayCard(validCard.gameObject);

		// Assert
		Assert.AreEqual(0, player.GetHandCount(), "Player did not successfully play the card.");
		Assert.AreEqual(validCard.gameObject, game.deck.getTopOfDiscard(), "The played card is not the new top of the discard pile.");
	}

	[Test]
	public void TestWinCondition()
	{
		// Arrange
		var gameObject = new GameObject();
		var game = gameObject.AddComponent<Game>();

		var playerObject = new GameObject("Player");
		var player = playerObject.AddComponent<Player>();
		player.name = "TestPlayer";

		game.players = new[] { player };
		game.currentPlayer = player;

		// Act
		game.checkForWin();

		// Assert
		Assert.IsTrue(game.gameOver, "The game did not end when the player had no cards.");
		Assert.AreEqual(player, game.winner, "The player with no cards was not declared the winner.");
	}
}
