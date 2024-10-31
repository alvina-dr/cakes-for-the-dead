using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bellows_Fire : MonoBehaviour
{
    public List<SpriteRenderer> SpriteRendererList = new List<SpriteRenderer>();
    public List<Sprite> SpriteList = new List<Sprite>();
    public int FireStrength;
    public float CookingDuration;
    public float CookingCurrentTime;
    private float scoreTotal;
    public UI_SliderValue TimerSlider;
    public UnityEvent onFireUp = new UnityEvent();
    public UnityEvent onFireDown = new UnityEvent();
    public UnityEvent onPerfectSoufflet = new UnityEvent();
    public UnityEvent onSouffletWind = new UnityEvent();

    private void Start()
    {
        scoreTotal = 0;
        SetFireSprite();
        AudioManager.Instance.onChaudronScene.Invoke();
    }

    private void Update()
    {
        ScoreManager.Instance.scoreTempActuel = Mathf.CeilToInt(scoreTotal);
        CookingCurrentTime += Time.deltaTime * (FireStrength);
        TimerSlider.SetBarValue(CookingCurrentTime, CookingDuration);
        if (CookingCurrentTime > CookingDuration)
        {
            CookingCurrentTime = 0;
            AudioManager.Instance.onChaudronSceneOff.Invoke();

            ScoreManager.Instance.CalculScore();
            GameManager.Instance.EndMiniGame(); 
        }
    }

    public void AddFireStrength()
    {
        FireStrength++;
        SetFireSprite();
        onFireUp.Invoke();
        onPerfectSoufflet.Invoke();
        onSouffletWind.Invoke();
        AudioManager.Instance.onSoufflet.Invoke();
        ScoreManager.Instance.AddMultiplicateur(1);
        scoreTotal += 100;
        AudioManager.Instance.onFireOn.Invoke();
        AudioManager.Instance.onPerfect.Invoke();
    }

    public void ResetFireStrength()
    {
        FireStrength = 0;
        SetFireSprite();
        onFireDown.Invoke();
        onSouffletWind.Invoke();
        ScoreManager.Instance.ResetMultiplicateur();
        AudioManager.Instance.onFireOff.Invoke();
        AudioManager.Instance.onSoufflet.Invoke();
        scoreTotal -= 50;
    }

    public void SetFireSprite()
    {
        if (FireStrength >= SpriteList.Count)
        {
            FireStrength = SpriteList.Count - 1;
            CookingCurrentTime += 3.0f;
        }

        for (int i = 0; i < SpriteRendererList.Count; i++)
        {
            if (SpriteRendererList[i].transform.parent.TryGetComponent<UI_ShowSizeAnimation>(out UI_ShowSizeAnimation sizeAnimation))
            {
                sizeAnimation.SetShowSize(1 + (FireStrength / 5.0f));
                sizeAnimation.Show();
                SpriteRendererList[i].sprite = SpriteList[FireStrength];
            }
        }
    }
}
