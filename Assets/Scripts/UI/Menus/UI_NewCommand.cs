using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPEffects.Components;

public class UI_NewCommand : MonoBehaviour
{
    [SerializeField] private UI_ShowSizeAnimation _characterAnimation;
    [SerializeField] private Image _characterImage;
    [SerializeField] private UI_ShowSizeAnimation _dialogAnimation;
    [SerializeField] private UI_ShowSizeAnimation _buttonAnimation;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private TMPWriter _dialogTextWriter;

    public void Open()
    {
        _dialogText.text = GameManager.Instance.CurrentCustomerData.Dialog;
        _characterImage.color = GameManager.Instance.CurrentCustomerData.CustomerSpriteColor;
        int rand = Random.Range(0,3);
        switch (rand)
        {
            case 0:
                AudioManager.Instance.onVoice1.Invoke();
                break;
            case 1:
                AudioManager.Instance.onVoice2.Invoke();
                break;
            case 2:
                AudioManager.Instance.onVoice3.Invoke();
                break;
        }
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
        AudioManager.Instance.onVoiceOff.Invoke();
        _characterAnimation.Hide();
        _dialogAnimation.Hide();
        _buttonAnimation.Hide(() => gameObject.SetActive(false));
    }
}
