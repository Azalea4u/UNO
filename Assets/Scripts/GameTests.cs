using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameTests
{
	[Test]
	public void TestPlayersStartWithSevenCards()
	{
		// Arrange
		var gameObject = new GameObject();
		var game = gameObject.AddComponent<Game>();
		GameObject handUI = new GameObject();

		game.players = new Player[2];
		for (int i = 0; i < 2; i++)
		{
			var playerObject = new GameObject($"Player{i}");
			game.players[i] = playerObject.AddComponent<Player>();
			game.players[i].GetComponent<Player>().PlayerHandUI = handUI;
		}

		var deckObject = new GameObject("Deck");
		game.deck = deckObject.AddComponent<Deck>();
		GameObject cardPrefab = new GameObject();
		cardPrefab.AddComponent<Card>();
		cardPrefab.GetComponent<Card>().Image = cardPrefab.AddComponent<Image>();
		cardPrefab.AddComponent<DragDrop>();
		game.deck.cardPrefab = cardPrefab;
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
		GameObject cardPrefab = new GameObject();
		cardPrefab.AddComponent<Card>();
		cardPrefab.GetComponent<Card>().Image = cardPrefab.AddComponent<Image>();
		cardPrefab.AddComponent<DragDrop>();
		game.deck.cardPrefab = cardPrefab;
		game.deck.InitializeDeck();

		GameObject handUI = new GameObject();
		var playerObject = new GameObject("Player");
		var player = playerObject.AddComponent<Player>();
		game.players = new[] { player };
		game.currentPlayer = player;
		game.currentPlayer.GetComponent<Player>().PlayerHandUI = handUI;
		game.currentPlayer.InitializePlayer();

		var topCard = new GameObject("TopCard").AddComponent<Card>();
		topCard.Type = CardType.Number;
		topCard.Number = 5;
		topCard.Color = "Red";
		topCard.GetComponent<Card>().Image = cardPrefab.AddComponent<Image>();
		topCard.gameObject.AddComponent<DragDrop>();

		var validCard = new GameObject("ValidCard").AddComponent<Card>();
		validCard.Type = CardType.Number;
		validCard.Number = 5; // Matches by number
		validCard.Color = "Blue"; // Different color
		validCard.GetComponent<Card>().Image = cardPrefab.AddComponent<Image>();
		validCard.gameObject.AddComponent<DragDrop>();

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
		player.InitializePlayer();
		game.currentPlayer = player;

		// Act
		game.checkForWin();

		// Assert
		Assert.IsTrue(game.gameOver, "The game did not end when the player had no cards.");
		Assert.AreEqual(player, game.winner, "The player with no cards was not declared the winner.");
	}
}