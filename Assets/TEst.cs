using UnityEngine;

public class TEst : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;


    void Update()
    {

        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        // Update the position of the cube
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);

    }
}
