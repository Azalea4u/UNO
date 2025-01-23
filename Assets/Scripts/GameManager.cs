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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playCard(int arrayIndex)
    {
        game.PlayCard(arrayIndex);
    }
}
