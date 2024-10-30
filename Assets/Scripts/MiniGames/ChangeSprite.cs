using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public IngredientData ingredientData;

    private void Start()
    {
        spriteRenderer.sprite = ingredientData.GrindedSpriteList[Random.Range(0,2)];
    }
}
