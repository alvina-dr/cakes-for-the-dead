using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bellows_MovingBar : MonoBehaviour
{
    public Bellows_Fire Fire;
    public UI_ShowSizeAnimation MovingCursor;
    public Image TargetZone;
    public UI_ShowSizeAnimation TargetZoneAnimation;
    public bool IsGoingRight = true;
    private float _leftMaxPosition;
    private float _rightMaxPosition;
    [SerializeField] private SpriteRenderer _bellowsSpriteRenderer;
    [SerializeField] private Sprite _bellowsOpen;
    [SerializeField] private Sprite _bellowsClose;
    public float CurrentCursorSpeed;
    [SerializeField] private List<float> _cursorSpeedList = new List<float>();
    public UI_ShowSizeAnimation FireIconAnimation;

    private void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        _rightMaxPosition = rectTransform.position.x + rectTransform.sizeDelta.x/2;
        _leftMaxPosition = rectTransform.position.x - rectTransform.sizeDelta.x/2;
        SetupTargetZone();
    }

    public void Update()
    {
        if (MovingCursor.transform.position.x >= _rightMaxPosition)
        {
            IsGoingRight = !IsGoingRight;
            MovingCursor.transform.position = new Vector3(_rightMaxPosition, MovingCursor.transform.position.y, 0);
        }

        if (MovingCursor.transform.position.x <= _leftMaxPosition)
        {
            IsGoingRight = !IsGoingRight;
            MovingCursor.transform.position = new Vector3(_leftMaxPosition, MovingCursor.transform.position.y, 0);
        }

        MovingCursor.transform.position += new Vector3(CurrentCursorSpeed * (IsGoingRight ? 1 : -1) * Time.deltaTime, 0, 0);
    }

    public void CheckCursorPosition()
    {
        MovingCursor.Show();
        FireIconAnimation.Show();
        StartCoroutine(CloseBellows());

        float cursorWidth = MovingCursor.GetComponent<Image>().rectTransform.sizeDelta.x;
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

    private IEnumerator CloseBellows()
    {
        _bellowsSpriteRenderer.sprite = _bellowsClose;
        yield return new WaitForSeconds(.5f);
        _bellowsSpriteRenderer.sprite = _bellowsOpen;
    }

    public void ValidateCursorPosition()
    {
        Fire.AddFireStrength();
        CurrentCursorSpeed = _cursorSpeedList[Fire.FireStrength];
        SetupTargetZone();
    }

    public void InvalidateCursorPosition()
    {
        Fire.ResetFireStrength();
        CurrentCursorSpeed = 0;
        DOVirtual.DelayedCall(.8f, () => CurrentCursorSpeed = _cursorSpeedList[Fire.FireStrength]);
    }

    public void SetupTargetZone()
    {
        TargetZoneAnimation.Hide(() =>
        {
            float totalWidth = _rightMaxPosition - _leftMaxPosition;
            float randomPosition = Random.Range(0.0f, totalWidth) - totalWidth / 2 + transform.position.x;
            TargetZone.transform.position = new Vector3(randomPosition, TargetZone.transform.position.y, 0);
            TargetZoneAnimation.Show();
        });
    }
}
