using UnityEngine;

public class Draggeable : MonoBehaviour
{
    Vector3 difference = Vector3.zero;
    [SerializeField]
    private float _followingSpeed;

    public void OnMouseDown()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference = new Vector3(difference.x, difference.y, 0);
    }

    public void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition + new Vector3(0, 0, 10);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(worldPosition.x, worldPosition.y, 0) - difference, Time.deltaTime * _followingSpeed);
    }
}
