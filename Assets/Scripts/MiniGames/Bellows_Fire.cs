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
    public UI_SliderValue TimerSlider;
    public UnityEvent onFireUp = new UnityEvent();
    public UnityEvent onFireDown = new UnityEvent();
    public UnityEvent onPerfectSoufflet = new UnityEvent();
    public UnityEvent onSouffletWind = new UnityEvent();

    private void Start()
    {
        SetFireSprite();
    }

    private void Update()
    {
        CookingCurrentTime += Time.deltaTime * (FireStrength);
        TimerSlider.SetBarValue(CookingCurrentTime, CookingDuration);
        if (CookingCurrentTime > CookingDuration)
        {
            CookingCurrentTime = 0;
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
        Multiplicateur.Instance.AddMultiplicateur(1);
    }

    public void ResetFireStrength()
    {
        FireStrength = 0;
        SetFireSprite();
        onFireDown.Invoke();
        onSouffletWind.Invoke();
        Multiplicateur.Instance.RemoveMultiplicateur(1);
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
            SpriteRendererList[i].sprite = SpriteList[FireStrength];
            SpriteRendererList[i].transform.parent.localScale = Vector3.one + (Vector3.one * FireStrength/5.0f);
        }
    }
}
