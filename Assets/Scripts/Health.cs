using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _min;
    [SerializeField] private int _max;
    public int Current {get; private set;}

    private void Awake()
    {
        Current = Max;
    }

    public int Max => _max;

    public void HealBy(int value)
    {
        Current = Mathf.Clamp(Current + value, _min, _max);
    }

    public void TakeDamage(int value)
    {
        Current = Mathf.Clamp(Current - value, _min, _max);
    }
}
