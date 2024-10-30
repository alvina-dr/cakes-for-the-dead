using DG.Tweening;
using UnityEngine;

public class DetectGrinding : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer CurrentIngredientSprite;
    public IngredientData CurrentIngredientData;
    public int GrindIndex = 0;

    private Camera mainCamera;
    [SerializeField]
    private float shakeForce;
    [SerializeField]
    private float shakeDuration;
    //private Sequence cameraShake;

    private void Start()
    {
        mainCamera = Camera.main;
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[0];
    }

    private void OnTriggerEnter(Collider collision)
    {
        mainCamera.transform.DOShakePosition(shakeDuration, shakeForce);
        Grind();
    }

    public void Grind()
    {
        GrindIndex++;
        if (GrindIndex >= CurrentIngredientData.GrindedSpriteList.Count)
        {
            GameManager.Instance.EndMiniGame();
            return;
        }
        
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[GrindIndex];
    }
}
