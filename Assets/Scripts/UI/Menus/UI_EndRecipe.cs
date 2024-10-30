using UnityEngine;
using UnityEngine.UI;

public class UI_EndRecipe : MonoBehaviour
{
    public UI_ShowSizeAnimation ScoreTextAnimation;
    public UI_TextValue ScoreTextValue;
    public UI_ShowSizeAnimation RecipeResultImageAnimation;
    public Image RecipeResultImage;

    public void SetResult()
    {
        RecipeResultImage.sprite = GameManager.Instance.CurrentRecipeData.CakeSprite;
        //ScoreTextValue = 
    }
}
