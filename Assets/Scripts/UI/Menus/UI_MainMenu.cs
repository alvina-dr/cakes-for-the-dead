using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    public UI_ShowSizeAnimation BackgroundAnimation;
    public UI_ShowSizeAnimation PlayButton;
    public UI_ShowSizeAnimation CakeShowAnimation;
    public UI_ShowSizeAnimation CustomerShowAnimation;
    public List<UI_ShowSizeAnimation> _creditNameList = new List<UI_ShowSizeAnimation>();
    [SerializeField] private CanvasGroup _canvasGroup;

    public void Open()
    {
        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => PlayButton.Show());
        animation.AppendInterval(1.0f);
        animation.AppendCallback(() => CakeShowAnimation.Show());
        animation.AppendInterval(0.1f);
        animation.AppendCallback(() => CustomerShowAnimation.Show());
        for (int i = 0; i < _creditNameList.Count; i++)
        {
            int index = i;
            animation.AppendInterval(.1f);
            animation.AppendCallback(() => _creditNameList[index].Show());
        }
    }

    public void Close()
    {
        _canvasGroup.DOFade(0, .3f);
        transform.DOScale(1.3f, .5f).OnComplete(() => { 
            gameObject.SetActive(false);
        });
    }
}
