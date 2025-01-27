using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameManager Instance { get; private set; }

	[SerializeField]
	Game game;

	[SerializeField]
	GameObject WildMenu;

	[SerializeField]
	private Image[] DiscardImgs;

	public Canvas Player01_UI;
	public Canvas Player02_UI;

	[SerializeField] private string currentPlayerTurn = "Player01";

	private GameObject WildCard;

	void Start()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // Keep the GameManager across scenes
		}
		else
		{
			Destroy(gameObject); // Destroy duplicate GameManager
		}

		ActivateDisplays();

		WildMenu.SetActive(false);
        Player01_UI.targetDisplay = 2;

        game.StartGame();
		UpdateDiscardPic();
	}

	private void Update()
	{
		if (currentPlayerTurn == "Player01")
		{
			Player01_UI.targetDisplay = 2;
			Player02_UI.targetDisplay = 1;
		}
		else if (currentPlayerTurn == "Player02")
		{
			Player01_UI.targetDisplay = 0;
			Player02_UI.targetDisplay = 2;
		}
	}

	private void ActivateDisplays()
	{
		// Activate all available displays
		for (int i = 0; i < Display.displays.Length; i++)
		{
			Display.displays[i].Activate();
		}
	}

	public void playCard(GameObject card)
	{
		if (card.GetComponent<Card>().Type == CardType.Wild || card.GetComponent<Card>().Type == CardType.Wild4)
		{
			WildCard = card;
			WildMenu.SetActive(true);
			foreach (GameObject ACard in game.currentPlayer.Hand)
			{
				ACard.GetComponent<DragDrop>().enabled = false;
			}
		}
		else
		{
			game.PlayCard(card);
			UpdateDiscardPic();
		}
	}

	public void UpdateDiscardPic()
	{
		foreach(Image img in DiscardImgs)
		{
			img.sprite = game.deck.getTopOfDiscard().GetComponent<Image>().sprite;
		}
	}

	public void BlueBtn()
	{
		WildCard.GetComponent<Card>().Color = "blue";
		game.PlayCard(WildCard);
		UpdateDiscardPic();
	}

	public void YellowBtn()
	{
		WildCard.GetComponent<Card>().Color = "yellow";
		game.PlayCard(WildCard);
		UpdateDiscardPic();
	}

	public void GreenBtn()
	{
		WildCard.GetComponent<Card>().Color = "green";
		game.PlayCard(WildCard);
		UpdateDiscardPic();
	}

	public void RedBtn()
	{
		WildCard.GetComponent<Card>().Color = "red";
		game.PlayCard(WildCard);
		UpdateDiscardPic();
	}
}
