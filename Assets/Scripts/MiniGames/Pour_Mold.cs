using TMPro;
using UnityEngine;

public class Pour_Mold : MonoBehaviour
{
    public LayerMask FluidLayer;
    private int fullnessIndex = 0;
    [SerializeField] private TextMeshPro _fullnessIndicator;
    private int denies;

    private void Start()
    {
        _fullnessIndicator.text = fullnessIndex.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((FluidLayer & (1 << collision.gameObject.layer)) != 0)
        {
            fullnessIndex++;
            _fullnessIndicator.text = fullnessIndex.ToString();

            if (fullnessIndex == 50)
            {
                ScoreManager.Instance.scoreTempActuel += 75;
                AudioManager.Instance.onPerfect.Invoke();
                ScoreManager.Instance.AddMultiplicateur(1);
            }
            if (fullnessIndex >= 55 && denies < 1)
            {
                ScoreManager.Instance.scoreTempActuel -= 25;
                denies++;
            }
            if (fullnessIndex >= 75 && denies < 2)
            {
                ScoreManager.Instance.scoreTempActuel -= 25;
                ScoreManager.Instance.ResetMultiplicateur();
                denies++;
            }
            if (fullnessIndex == 125)
            {   
                ScoreManager.Instance.scoreTempActuel += 1000;
                ScoreManager.Instance.AddMultiplicateur(20);
            }
        }
    }
}
