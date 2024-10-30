using System.Collections.Generic;
using UnityEngine;

public class Bellows_Fire : MonoBehaviour
{
    public List<SpriteRenderer> SpriteRendererList = new List<SpriteRenderer>();
    public List<Sprite> SpriteList = new List<Sprite>();
    public int FireStrength;
    public float CookingDuration;
    public float CookingCurrentTime;
    public UI_SliderValue TimerSlider;

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
    }

    public void ResetFireStrength()
    {
        FireStrength = 0;
        SetFireSprite();
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
