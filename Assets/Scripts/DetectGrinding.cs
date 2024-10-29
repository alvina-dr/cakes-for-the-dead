using UnityEngine;

public class DetectGrinding : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer CurrentIngredientSprite;
    public IngredientData CurrentIngredientData;
    public int GrindIndex = 0;

    private void Start()
    {
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[0];
    }

    private void OnTriggerEnter(Collider collision)
    {
        Grind();
    }

    public void Grind()
    {
        GrindIndex++;
        if (GrindIndex >= CurrentIngredientData.GrindedSpriteList.Count)
        {
            MG_GrindManager.Instance.EndGame();
            return;
        }
        
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[GrindIndex];
    }
}
