using DG.Tweening;
using UnityEngine;

public class UI_NewCommand : MonoBehaviour
{
    [SerializeField] private UI_ShowSizeAnimation _characterAnimation;
    [SerializeField] private UI_ShowSizeAnimation _dialogAnimation;
    [SerializeField] private UI_ShowSizeAnimation _buttonAnimation;

    public void Open()
    {
        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => _characterAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => _dialogAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => _buttonAnimation.Show());
        //show recipe result image + text
    }

    public void Hide()
    {
        _characterAnimation.Hide();
        _dialogAnimation.Hide();
        _buttonAnimation.Hide(() => gameObject.SetActive(false));
    }
}
