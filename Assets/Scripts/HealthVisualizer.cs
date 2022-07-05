using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Health))]

public class HealthVisualizer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeTimeInSeconds;
    [SerializeField] private TMP_Text _textOfHealth;
    [SerializeField] private Color _lowHealthColor;
    [SerializeField] private Color _maxHealthColor;
    private Health _health;
    private Coroutine _displayChangeCoroutine;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _slider.maxValue = _health.Max;
        _slider.value = _slider.maxValue;
        _textOfHealth.text = _health.Max.ToString();
        _textOfHealth.color = _maxHealthColor;
    }

    public void RefreshHealthDisplay()
    {
        StopActiveCoroutine();
        _displayChangeCoroutine = StartCoroutine(SmoothDisplayChange());
    }

    private IEnumerator SmoothDisplayChange()
    {
        float changeRate = Mathf.Abs(_slider.value - _health.Current) / _changeTimeInSeconds;

        while (_slider.value != _health.Current)
        {
            ChangeSliderValueWithStep(changeRate * Time.deltaTime);
            ChangeText();

            yield return null;
        }
    }

    private void ChangeSliderValueWithStep(float step)
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _health.Current, step);
    }

    private void ChangeText()
    {
        _textOfHealth.text = ((int)_slider.value).ToString();
        _textOfHealth.color = Color.Lerp(_lowHealthColor, _maxHealthColor, _slider.normalizedValue);
    }

    private void StopActiveCoroutine()
    {
        if (_displayChangeCoroutine != null)
            StopCoroutine(_displayChangeCoroutine);
    }
}
