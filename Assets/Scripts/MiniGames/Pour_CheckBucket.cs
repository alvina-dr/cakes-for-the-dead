using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour_CheckBucket : MonoBehaviour
{
    public LayerMask FluidLayer;
    [SerializeField]
    private int _numDrops;
    private int _currentNumDrop;
    private bool waterSoundOn = false;
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
        yield return new WaitForSeconds(2.0f);
        if (GameManager.Instance != null)
        {

            ScoreManager.Instance.CalculScore();
            GameManager.Instance.EndMiniGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!waterSoundOn)
        {
            AudioManager.Instance.onPour.Invoke();
            waterSoundOn = true;
        }
        if ((FluidLayer & (1 << collision.gameObject.layer)) != 0)
        {
            _currentNumDrop++;
        }
    }
}
