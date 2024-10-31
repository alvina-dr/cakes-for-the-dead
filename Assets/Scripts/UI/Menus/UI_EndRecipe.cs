using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndRecipe : MonoBehaviour
{
    public UI_ShowSizeAnimation ScoreTextAnimation;
    public UI_TextValue ScoreTextValue;
    public UI_ShowSizeAnimation RecipeResultImageAnimation;
    public Image RecipeResultImage;
    public UI_ShowSizeAnimation CloseButtonAnimation;
    public UI_ShowSizeAnimation CakeTitleAnimation;
    public UI_TextValue CakeTitleText;
    public void Open()
    {
        RecipeResultImage.sprite = GameManager.Instance.CurrentCustomerData.Recipe.CakeSprite;
        ScoreTextValue.SetTextValue("+" + ScoreManager.Instance.scoreTempActuel.ToString());
        CakeTitleText.SetTextValue("<grow>" + GameManager.Instance.CurrentCustomerData.Recipe.CakeName);

        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => RecipeResultImageAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => CakeTitleAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => ScoreTextAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() => CloseButtonAnimation.Show());
        animation.AppendInterval(.3f);
        animation.AppendCallback(() =>
        {
            ScoreManager.Instance.TotalScore += ScoreManager.Instance.scoreTempActuel;
            GameManager.Instance.UIManager.ScoreUI.SetTextValue(ScoreManager.Instance.TotalScore.ToString());
        });
    }

    public void Close()
    {
        RecipeResultImageAnimation.Hide();
        ScoreTextAnimation.Hide();
        CloseButtonAnimation.Hide(() => gameObject.SetActive(false));
    }
}
