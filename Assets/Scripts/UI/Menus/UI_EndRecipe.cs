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

    public void Open()
    {
        RecipeResultImage.sprite = GameManager.Instance.CurrentRecipeData.CakeSprite;
        //ScoreTextValue = 
        
        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(1.0f);
        animation.AppendCallback(() => RecipeResultImageAnimation.Show());
        animation.AppendInterval(1.0f);
        animation.AppendCallback(() => ScoreTextAnimation.Show());
        animation.AppendInterval(1.0f);
        animation.AppendCallback(() => CloseButtonAnimation.Show());
    }

    public void Close()
    {
        RecipeResultImageAnimation.Hide();
        ScoreTextAnimation.Hide();
        CloseButtonAnimation.Hide(() => gameObject.SetActive(false));
    }
}
