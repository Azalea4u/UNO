using Unity.VisualScripting;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;
    private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void BeginDrag()
    {
        isDragging = true;
    }

    public void EndDrag()
    {
        isDragging = false;

        if(this.transform.position.y > 250)
        {
            //gameManager.PlayCardRPC(gameObject);
        }
    }
}
