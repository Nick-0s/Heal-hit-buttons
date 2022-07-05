using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeTimeInSeconds;
    [SerializeField] private TMP_Text _textOfHealth;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Color _lowHealthColor;
    [SerializeField] private Color _maxHealthColor;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _maxHealth;
        _slider.value = _slider.maxValue;
        _textOfHealth.text = _currentHealth.ToString();
        _textOfHealth.color = _maxHealthColor;
    }

    public void ChangeHealthValueBy(int value)
    {
        StartCoroutine(ChangeHealthBy(value));
    }

    private IEnumerator ChangeHealthBy(int value)
    {
        float changeRate = Mathf.Abs(value / _changeTimeInSeconds);

        _currentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);

        while (_slider.value != _currentHealth)
        {
            ChangeSliderValueWithStep(changeRate * Time.deltaTime);
            ChangeText();

            yield return null;
        }
    }

    private void ChangeSliderValueWithStep(float value)
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _currentHealth, value);
    }

    private void ChangeText()
    {
        _textOfHealth.text = ((int)_slider.value).ToString();
        _textOfHealth.color = Color.Lerp(_lowHealthColor, _maxHealthColor, _slider.normalizedValue);
    }
}
