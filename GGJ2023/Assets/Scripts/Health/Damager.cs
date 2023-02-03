using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CanAffect
{
    Tree,
    Enemy
}
public class Damager : MonoBehaviour
{
    [SerializeField] private CanAffect _canAffect;
    [SerializeField] private int _damageAmount;

    internal CanAffect CanAffect { get { return _canAffect; } }
    public int DamageAmount { get { return _damageAmount; } }
}
