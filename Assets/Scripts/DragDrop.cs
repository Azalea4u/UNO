using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;

    private void Start()
    {
        
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
    }
}
