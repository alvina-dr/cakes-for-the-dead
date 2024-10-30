using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_SliderValue : MonoBehaviour
{
    [SerializeField] private Slider bar;

    public void SetBarValue(float _currentValue, float _maxValue)
    {
        bar.DOValue(_currentValue / _maxValue, .2f);
    }
}
