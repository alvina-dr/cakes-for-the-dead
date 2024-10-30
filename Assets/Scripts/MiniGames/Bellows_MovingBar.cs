using UnityEngine;
using UnityEngine.UI;

public class Bellows_MovingBar : MonoBehaviour
{
    public Bellows_Fire Fire;
    public Image MovingCursor;
    public float CursorSpeed;
    public Image TargetZone;
    public bool IsGoingRight = true;
    private float _leftMaxPosition;
    private float _rightMaxPosition;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        _rightMaxPosition = rectTransform.position.x + rectTransform.sizeDelta.x/2;
        _leftMaxPosition = rectTransform.position.x - rectTransform.sizeDelta.x/2;
        SetupTargetZone();
    }


    public void Update()
    {
        if (MovingCursor.transform.position.x > _rightMaxPosition || MovingCursor.transform.position.x < _leftMaxPosition)
        {
            IsGoingRight = !IsGoingRight;
        }

        MovingCursor.transform.position += new Vector3(CursorSpeed * (IsGoingRight ? 1 : -1), 0, 0);
    }

    public void CheckCursorPosition()
    {
        float cursorWidth = MovingCursor.rectTransform.sizeDelta.x;
        float rightMovingCursor = MovingCursor.transform.position.x + cursorWidth / 2;
        float leftMovingCursor = MovingCursor.transform.position.x - cursorWidth / 2;

        float targetWidth = TargetZone.rectTransform.sizeDelta.x;
        float rightTargetZone = TargetZone.transform.position.x + targetWidth / 2;
        float leftTargetZone = TargetZone.transform.position.x - targetWidth / 2;

        if (rightMovingCursor < rightTargetZone && rightMovingCursor > leftTargetZone)
        {
            ValidateCursorPosition();
            return;
        }

        if (leftMovingCursor > leftTargetZone && leftMovingCursor < rightTargetZone)
        {
            ValidateCursorPosition();
            return;
        }

        InvalidateCursorPosition();
    }

    public void ValidateCursorPosition()
    {
        Fire.AddFireStrength();
        SetupTargetZone();
    }

    public void InvalidateCursorPosition()
    {
        Fire.ResetFireStrength();
    }

    public void SetupTargetZone()
    {
        float totalWidth = _rightMaxPosition - _leftMaxPosition;
        float randomPosition = Random.Range(0.0f, totalWidth) - totalWidth / 2 + transform.position.x;
        TargetZone.transform.position = new Vector3(randomPosition, TargetZone.transform.position.y, 0);
    }
}
