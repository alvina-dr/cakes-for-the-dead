using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndDay : MonoBehaviour
{
    public UI_ShowSizeAnimation RentTicketAnimation;
    public UI_ShowSizeAnimation CustomerCountAnimation;
    public UI_TextValue CustomerCountText;
    public UI_ShowSizeAnimation CurrentMoneyAnimation;
    public UI_TextValue CurrentMoneyText;
    public UI_ShowSizeAnimation RentValueAnimation;
    public UI_TextValue RentValueText;
    public UI_ShowSizeAnimation MoneyLeftAnimation;
    public UI_TextValue MoneyLeftText;
    public UI_ShowSizeAnimation ButtonNextDayAnimation;

    public void Open()
    {
        //CustomerCountText = 

        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => RentTicketAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => CustomerCountAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => CurrentMoneyAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => RentValueAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => MoneyLeftAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => ButtonNextDayAnimation.Show());
    }

    public void Close()
    {
        RentTicketAnimation.Hide();
        CurrentMoneyAnimation.Hide();
        CustomerCountAnimation.Hide();
        RentValueAnimation.Hide();
        MoneyLeftAnimation.Hide();
        ButtonNextDayAnimation.Hide(() => gameObject.SetActive(false));
    }
}
