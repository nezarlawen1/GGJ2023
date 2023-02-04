using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public static HealthHandler Instance;

    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _currentHP;
    [SerializeField] private HealthDisplay _healthDisplay;

    [SerializeField] private List<GameObject> _portalRefs = new List<GameObject>();
    [SerializeField] private PortalSpawnerManager _portalSpawnerManager;
    [SerializeField] private int _damageRate = 1;
    [SerializeField] private float _damageInterval = 1;
    private float _damageTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _currentHP = _maxHP;
        _healthDisplay.Setup(this);
        for (int i = 0; i < _portalSpawnerManager.spawnPortalList.Count; i++)
        {
            _portalRefs.Add(null);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _portalRefs.Count; i++)
        {
            _portalRefs[i] = _portalSpawnerManager.spawnPortalList[i].newPortal;
        }
        DamageOverTime();
    }

    private void DamageOverTime()
    {
        int count = 0;
        foreach (var item in _portalRefs)
        {
            if (item != null)
            {
                count++;
            }
        }

        if (_damageTimer >= _damageInterval)
        {
            _damageTimer = 0;
            Damage(_damageRate * count);
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
