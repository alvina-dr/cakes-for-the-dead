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
    private float scoreTotal;

    public UnityEvent onGrind = new UnityEvent();

    [SerializeField]
    private float timerMulti;
    [SerializeField]
    private float finalScoreTweaker;

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
        ScoreManager.Instance.AddMultiplicateur(1);
        onGrind.Invoke();
        GrindIndex++;
        if (GrindIndex >= CurrentIngredientData.GrindedSpriteList.Count)
        {
            CheckForCombo();
            CheckForScore();
            ScoreManager.Instance.scoreTempActuel = Mathf.CeilToInt(scoreTotal);
            GameManager.Instance.EndMiniGame();
            return;
        }

        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[GrindIndex];
    }

    public void CheckForCombo()
    {
        if (timerMulti < 10)
        {
            ScoreManager.Instance.AddMultiplicateur(2);
        }
        else
        {
            ScoreManager.Instance.AddMultiplicateur(0);
        }
        timerMulti = 0;
    }

    private void CheckForScore()
    {
        scoreTotal = (100 - timerMulti) * finalScoreTweaker;
    }
}