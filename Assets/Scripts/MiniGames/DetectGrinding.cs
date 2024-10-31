using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DetectGrinding : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer CurrentIngredientSprite;
    public IngredientData CurrentIngredientData;
    public UI_ShowSizeAnimation Pestle;
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

    [SerializeField]
    private Material sparkMat;
    [SerializeField]
    private ParticleSystem grindParticleSsystem;

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
        
        GrindIndex++;

        if (GrindIndex == CurrentIngredientData.GrindedSpriteList.Count)
        {
            //start particles
            CheckForCombo();
            CheckForScore();
            grindParticleSsystem.GetComponent<ParticleSystemRenderer>().material = sparkMat;
            ScoreManager.Instance.scoreTempActuel = Mathf.CeilToInt(scoreTotal);
            Pestle.Hide(() => Destroy(Pestle.gameObject));
            StartCoroutine(EndMiniGameAnimation());
        }
        onGrind.Invoke();

        if (GrindIndex >= CurrentIngredientData.GrindedSpriteList.Count) return;

        CurrentIngredientSprite.sprite = CurrentIngredientData.GrindedSpriteList[GrindIndex];
    }

    private IEnumerator EndMiniGameAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndMiniGame();
        }
    }

    public void CheckForCombo()
    {
        if (timerMulti < 10)
        {
            ScoreManager.Instance.scoreTempActuel += 100;
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