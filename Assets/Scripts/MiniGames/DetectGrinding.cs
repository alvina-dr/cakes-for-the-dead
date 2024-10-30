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

    [SerializeField]
    private float timerMulti;

    private void Start()
    {
        mainCamera = Camera.main;
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[0];
    }
   
    private void Update()
    {
        timerMulti += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        mainCamera.transform.DOShakePosition(shakeDuration, shakeForce);
        Grind();
    }

    public void Grind()
    {
        CheckForCombo();
        onGrind.Invoke();
        GrindIndex++;
        if (GrindIndex >= CurrentIngredientData.GrindedSpriteList.Count)
        {
            Multiplicateur.Instance.AddMultiplicateur(1);
            GameManager.Instance.EndMiniGame();
            return;
        }
        
        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[GrindIndex];
    }

    public void CheckForCombo()
    {
        if (timerMulti < 1)
        {
            Multiplicateur.Instance.AddMultiplicateur(1);
        }
        else
        {
            Multiplicateur.Instance.AddMultiplicateur(0);
        }
        timerMulti = 0;
    }
}
