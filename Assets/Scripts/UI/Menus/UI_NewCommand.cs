using DG.Tweening;
using TMPro;
using UnityEngine;
using TMPEffects;
using TMPEffects.Components;

public class UI_NewCommand : MonoBehaviour
{
    [SerializeField] private UI_ShowSizeAnimation _characterAnimation;
    [SerializeField] private UI_ShowSizeAnimation _dialogAnimation;
    [SerializeField] private UI_ShowSizeAnimation _buttonAnimation;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private TMPWriter _dialogTextWriter;

    public void Open()
    {
        _dialogText.text = GameManager.Instance.CurrentCustomerData.Dialog;
        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => _characterAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => _dialogAnimation.Show());
        animation.AppendCallback(() => _dialogTextWriter.StartWriter());
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
