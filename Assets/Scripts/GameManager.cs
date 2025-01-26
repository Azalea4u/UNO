using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameManager Instance { get; private set; }

    [SerializeField]
    Game game;

    [Header("Game States")]
    public string CurrentTurn = "Player01";

    public Camera Player01_Camera;
    public Camera Player02_Camera;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CurrentTurn == "Player01")
            {
                CurrentTurn = "Player02";
            }
            else if (CurrentTurn == "Player02")
            {
                CurrentTurn = "Player01";
            }
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

    public void playCard(int arrayIndex)
    {
        game.PlayCard(arrayIndex);
    }
}
