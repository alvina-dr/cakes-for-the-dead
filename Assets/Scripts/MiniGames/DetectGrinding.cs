using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onGrind = new UnityEvent();

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
            onGrind.Invoke();
            GameManager.Instance.EndMiniGame();
            return;
        }
        
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[GrindIndex];
    }
}
