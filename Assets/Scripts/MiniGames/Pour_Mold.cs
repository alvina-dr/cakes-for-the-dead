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

            if (fullnessIndex == 60)
            {
                ScoreManager.Instance.scoreTempActuel += 100;
                AudioManager.Instance.onPerfect.Invoke();
                ScoreManager.Instance.AddMultiplicateur(1);
            }
            if (fullnessIndex == 125)
            {   
                ScoreManager.Instance.scoreTempActuel += 200;
                ScoreManager.Instance.AddMultiplicateur(3);
            }
        }
    }
}
