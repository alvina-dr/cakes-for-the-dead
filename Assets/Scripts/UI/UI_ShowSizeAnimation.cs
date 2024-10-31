using System;
using UnityEngine;
using DG.Tweening;

public class UI_ShowSizeAnimation : MonoBehaviour
{
    [SerializeField] private float _showSize;
    [SerializeField] private bool _showOnStart;

    private void Start()
    {
        if (!_showOnStart)
        {
            transform.localScale = Vector3.zero;
        }
    }

    public void Show(Action callback = null)
    {
        transform.DOKill();
        Sequence sizeUp = DOTween.Sequence();
        sizeUp.SetUpdate(false);
        sizeUp.Append(transform.DOScale(_showSize + (_showSize * .1f), .2f));
        sizeUp.Append(transform.DOScale(_showSize, .1f));
        sizeUp.AppendCallback(() => callback?.Invoke());
    }

    public void Hide(Action callback = null)
    {
        transform.DOKill();
        Sequence sizeUp = DOTween.Sequence();
        sizeUp.SetUpdate(false);
        sizeUp.Append(transform.DOScale(_showSize + (_showSize * .1f), .2f));
        sizeUp.Append(transform.DOScale(0, .1f));
        sizeUp.AppendCallback(() => callback?.Invoke());
    }

    public void SetShowSize(float newSize)
    {
        _showSize = newSize;
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
