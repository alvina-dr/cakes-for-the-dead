using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

public class Pour_Mold : MonoBehaviour
{
    public LayerMask FluidLayer;
    private int fullnessIndex = 0;
    [SerializeField] private TextMeshPro _fullnessIndicator;
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
        }
    }
}
