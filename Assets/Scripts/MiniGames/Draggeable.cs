using DG.Tweening;
using UnityEngine;

public class Draggeable : MonoBehaviour
{
    private Vector3 difference = Vector3.zero;
    [SerializeField] private SpriteRenderer _spriteSizeAnimation;
    [SerializeField] private float _inHandSize;
    private float _initialSize;

    private void Start()
    {
        _initialSize = _spriteSizeAnimation.transform.localScale.x;
    }

    public void OnMouseDown()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference = new Vector3(difference.x, difference.y, 0);
        if (_spriteSizeAnimation != null)
        {
            _spriteSizeAnimation.transform.DOScale(_inHandSize, .2f);
        }
    }

    public void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition + new Vector3(0, 0, 10);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0) - difference;
    }

    private void OnMouseUp()
    {
        if (_spriteSizeAnimation != null)
        {
            _spriteSizeAnimation.transform.DOScale(_initialSize, .2f);
        }
    }
}
