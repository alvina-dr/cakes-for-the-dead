using DG.Tweening;
using UnityEngine;

public class UI_StartDay : MonoBehaviour
{
    public UI_ShowSizeAnimation BookAnimation;
    public UI_ShowSizeAnimation ButtonStartDayAnimation;

    public void Open()
    {
        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => BookAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => ButtonStartDayAnimation.Show());
    }

    public void Hide()
    {
        ButtonStartDayAnimation.transform.DOKill();
        BookAnimation.Hide();
        ButtonStartDayAnimation.Hide(() => gameObject.SetActive(false));
    }
}
