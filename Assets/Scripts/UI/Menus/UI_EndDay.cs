using DG.Tweening;
using TMPro;
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
    public UI_ShowSizeAnimation ButtonGameOverAnimation;
    public UI_ShowSizeAnimation EvictionNoticeAnimation;
    public UI_ShowSizeAnimation ResultTitleAnimation;
    [SerializeField] private string _winTextFX;
    [SerializeField] private string _looseTextFX;

    public void Open()
    {
        CustomerCountText.SetTextValue(ScoreManager.Instance.NumberOfCustomerDay.ToString(), false);
        CurrentMoneyText.SetTextValue(ScoreManager.Instance.TotalScore.ToString(), false);
        RentValueText.SetTextValue(GameManager.Instance.RentDue.ToString(), false);
        int moneyLeft = ScoreManager.Instance.TotalScore - GameManager.Instance.RentDue;

        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => RentTicketAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => ResultTitleAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => CustomerCountAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => CurrentMoneyAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => RentValueAnimation.Show());
        animation.AppendInterval(.1f);
        animation.AppendCallback(() => MoneyLeftAnimation.Show());

        if (moneyLeft >= 0)
        {
            MoneyLeftText.GetComponent<TextMeshProUGUI>().color = Color.green;
            ResultTitleAnimation.GetComponent<TextMeshProUGUI>().text = "<grow>Rent paid !";
            animation.AppendInterval(.1f);
            animation.AppendCallback(() => ButtonNextDayAnimation.Show());
            ScoreManager.Instance.TotalScore = moneyLeft;
            MoneyLeftText.SetTextValue(_winTextFX + moneyLeft.ToString(), false);
        }
        else
        {
            MoneyLeftText.GetComponent<TextMeshProUGUI>().color = Color.red;
            ResultTitleAnimation.GetComponent<TextMeshProUGUI>().text = "<grow>Evicted !";
            MoneyLeftText.SetTextValue(_looseTextFX + moneyLeft.ToString(), false);
            animation.AppendInterval(.1f);
            animation.AppendCallback(() => ButtonGameOverAnimation.Show());
            animation.AppendInterval(.5f);
            animation.AppendCallback(() => EvictionNoticeAnimation.Show());
        }
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
