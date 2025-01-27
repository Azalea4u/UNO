using System.Collections;
using TMPro;
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
	public GameObject WildCard_Menu;

	[SerializeField]
	private Image[] DiscardImgs;

	[Header("PlayerUI")]
	[SerializeField] private Canvas Player01_UI;
	[SerializeField] private Canvas Player02_UI;
	[SerializeField] private GameObject CardHidden;
	[SerializeField] private GameObject Player1OPP_Hand;
	[SerializeField] private GameObject Player2OPP_Hand;
	[SerializeField] private TextMeshProUGUI currentTurn;

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

		WildCard_Menu.SetActive(false);
        //Player01_UI.targetDisplay = 2;

        game.StartGame();
		UpdateDiscardPic();
		SwitchPlayerScreen();
	}

	private void Update()
	{

	}

    private void UpdateOpponentHand(Player opponent, GameObject opponentHandUI)
    {
        if (opponent != null && opponent.Hand != null)
        {
            // Get the opponent's hand count
            int opponentHandCount = opponent.GetHandCount();

            // Get the current number of CardHidden objects in the opponent's hand UI
            int currentCardHiddenCount = opponentHandUI.transform.childCount;

            // Add CardHidden objects if there are fewer than the opponent's hand count
            while (currentCardHiddenCount < opponentHandCount)
            {
                GameObject cardHidden = Instantiate(CardHidden, opponentHandUI.transform);
                cardHidden.SetActive(true); // Ensure the card is visible
                currentCardHiddenCount++;
            }

            // Remove CardHidden objects if there are more than the opponent's hand count
            while (currentCardHiddenCount > opponentHandCount)
            {
                Transform lastChild = opponentHandUI.transform.GetChild(currentCardHiddenCount - 1);
                Destroy(lastChild.gameObject);
                currentCardHiddenCount--;
            }
        }
    }

    public void SwitchPlayerScreen()
    {
        if (game.currentPlayerIndex == 0)
        {
            Player01_UI.targetDisplay = 2;
            Player02_UI.targetDisplay = 1;

            currentTurn.text = "Player 1's Turn";

            // Update Player 2's opponent hand (Player 1's perspective)
            UpdateOpponentHand(game.players[1], Player1OPP_Hand);
        }
        else if (game.currentPlayerIndex == 1)
        {
            Player01_UI.targetDisplay = 0;
            Player02_UI.targetDisplay = 2;

            currentTurn.text = "Player 2's Turn";

            // Update Player 1's opponent hand (Player 2's perspective)
            UpdateOpponentHand(game.players[0], Player2OPP_Hand);
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
            WildCard_Menu.SetActive(true);
            foreach (GameObject ACard in game.currentPlayer.Hand)
            {
                ACard.GetComponent<DragDrop>().enabled = false;
            }
        }
        else
        {
            game.PlayCard(card);
            StartCoroutine(DelayScreenSwitch());
        }
    }

    private IEnumerator DelayScreenSwitch()
    {
        yield return new WaitForSeconds(0.25f); // Add a delay of 1.5 seconds
        UpdateDiscardPic();
		SwitchPlayerScreen();
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
        WildCard_Menu.SetActive(false);
		UpdateDiscardPic();

        StartCoroutine(DelayScreenSwitch());
    }

    public void YellowBtn()
	{
		WildCard.GetComponent<Card>().Color = "yellow";
		game.PlayCard(WildCard);
        WildCard_Menu.SetActive(false);
		UpdateDiscardPic();

        StartCoroutine(DelayScreenSwitch());
    }

    public void GreenBtn()
	{
		WildCard.GetComponent<Card>().Color = "green";
		game.PlayCard(WildCard);
        WildCard_Menu.SetActive(false);
		UpdateDiscardPic();

        StartCoroutine(DelayScreenSwitch());
    }

    public void RedBtn()
	{
		WildCard.GetComponent<Card>().Color = "red";
		game.PlayCard(WildCard);
        WildCard_Menu.SetActive(false);
		UpdateDiscardPic();

        StartCoroutine(DelayScreenSwitch());
    }
}
