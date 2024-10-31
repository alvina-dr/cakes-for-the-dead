using TMPro;
using UnityEngine;

public class Pour_Mold : MonoBehaviour
{
    public LayerMask FluidLayer;
    private int fullnessIndex = 0;
    [SerializeField] private TextMeshPro _fullnessIndicator;
    private int denies;
    public bool fullnessbool = false;
    public bool waterFalled = false;
    public int fullnessReminder;
    public float compteur;

    private void Start()
    {
        _fullnessIndicator.text = fullnessIndex.ToString();
    }

    private void Update()
    {
        if (fullnessbool == false)
        {
            compteur += Time.deltaTime;
            if (fullnessIndex != fullnessReminder)
            { compteur = 0; }
            if ( compteur > 0.2f)
            {
                AudioManager.Instance.onPourOff.Invoke();
                fullnessbool = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fullnessbool)
        {
            AudioManager.Instance.onPour.Invoke();
            fullnessbool = false;
        }

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
