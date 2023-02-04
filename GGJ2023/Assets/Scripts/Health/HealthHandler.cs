using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _currentHP;
    [SerializeField] private HealthDisplay _healthDisplay;

    [SerializeField] private List<GameObject> _portalRefs = new List<GameObject>();
    [SerializeField] private int _damageRate = 1;
    [SerializeField] private float _damageInterval = 1;
    private float _damageTimer;

    private void Awake()
    {
        _currentHP = _maxHP;
        _healthDisplay.Setup(this);
    }

    private void Update()
    {
        DamageOverTime();
    }

    private void DamageOverTime()
    {
        if (_damageTimer >= _damageInterval)
        {
            _damageTimer = 0;
            Damage(_damageRate * _portalRefs.Count);
        }
        else
        {
            _damageTimer += Time.deltaTime;
        }
    }

    public float GetHealthPercent()
    {
        return (float)_currentHP / _maxHP;
    }

    public void Damage(int damageAmount)
    {
        _currentHP -= damageAmount;
        if (_currentHP <= 0)
        {
            _currentHP = 0;
            if (OnDeath != null) OnDeath(this, EventArgs.Empty);
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        _currentHP += healAmount;
        if (_currentHP >= _maxHP)
        {
            _currentHP = _maxHP;
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void RefillHealth()
    {
        _currentHP = _maxHP;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
}
