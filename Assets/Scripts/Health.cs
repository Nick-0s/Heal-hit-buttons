using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _min;
    [SerializeField] private int _max;
    public int Current {get; private set;}
    public event UnityAction Changed;

    private void Awake()
    {
        Current = Max;
    }

    public int Max => _max;

    public void HealBy(int value)
    {
        Current = Mathf.Clamp(Current + value, _min, _max);
        Changed?.Invoke();
    }

    public void TakeDamage(int value)
    {
        Current = Mathf.Clamp(Current - value, _min, _max);
        Changed?.Invoke();
    }
}
