using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour_CheckBucket : MonoBehaviour
{
    public LayerMask FluidLayer;
    [SerializeField]
    private int _numDrops;
    private int _currentNumDrop;

    public void Update()
    {
        if (_currentNumDrop == _numDrops)
        {
            _currentNumDrop = 0;
            StartCoroutine(WaitDropFalling());
        }
    }

    private IEnumerator WaitDropFalling()
    {
        Debug.Log("End game");
        yield return new WaitForSeconds(4.0f);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndMiniGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((FluidLayer & (1 << collision.gameObject.layer)) != 0)
        {
            _currentNumDrop++;
        }
    }
}
