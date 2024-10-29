using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RecipeRequest : MonoBehaviour
{
    public TextMeshProUGUI CakeName;
    public Image CakeImage;

    public void SetupRecipeRequest(RecipeData data)
    {
        CakeName.text = data.name;
        CakeImage.sprite = data.CakeSprite;
    }
}
