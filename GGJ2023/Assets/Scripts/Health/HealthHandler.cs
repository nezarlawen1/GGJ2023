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
    [SerializeField] private bool isPlayer;
    [SerializeField] private EnemyAI EnemyAIRef;

    private void Awake()
    {
        EnemyAIRef = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float GetHealthPercent()
    {
        return (float)_currentHP / _maxHP;
    }

    private void TakeDamage(GameObject damagerObj)
    {
        Damager tempDamager = damagerObj.GetComponent<Damager>();

        if (gameObject.tag == "Tree" && tempDamager.CanAffect == CanAffect.Tree || gameObject.tag == "Enemy" && tempDamager.CanAffect == CanAffect.Enemy)
        {
            Damage(tempDamager.DamageAmount);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damager"))
        {
            TakeDamage(other.gameObject);
        }
    }
}
