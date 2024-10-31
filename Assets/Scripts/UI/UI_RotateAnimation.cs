using DG.Tweening;
using UnityEngine;

public class UI_RotateAnimation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotation;
    [SerializeField]
    private float _animationDuration;

    void Start()
    {
        transform.DORotate(_rotation, _animationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
