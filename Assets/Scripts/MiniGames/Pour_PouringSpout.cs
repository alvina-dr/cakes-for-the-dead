using UnityEngine;

public class Pour_PouringSpout : MonoBehaviour
{
    [SerializeField] private float _leftMax;
    [SerializeField] private float _rightMax;
    [SerializeField] private float _bucketSpeed;
    private bool _isGoingRight;
    private bool _isStopped;
    private Vector3 _initialMousePosition;
    private Vector3 _savedRotation;

    public void Update()
    {
        if (_isStopped) return;

        if (transform.position.x > _rightMax || transform.position.x < _leftMax)
        {
            _isGoingRight = !_isGoingRight;
        }

        transform.position += new Vector3(_bucketSpeed * (_isGoingRight ? 1 : -1), 0, 0);
    }

    public void OnMouseDown()
    {
        _initialMousePosition = Input.mousePosition;
        _isStopped = true;
    }

    public void OnMouseDrag()
    {
        transform.eulerAngles = new Vector3(0, 0, Input.mousePosition.x - _initialMousePosition.x) + _savedRotation;
    }

    private void OnMouseUp()
    {
        _savedRotation = transform.eulerAngles;
        _isStopped = false;
    }
}
