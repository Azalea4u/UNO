using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Game game;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game.StartGame();
    }

    public void playCard(int arrayIndex)
    {
        game.PlayCard(arrayIndex);
    }
}
