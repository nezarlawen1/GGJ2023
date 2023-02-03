using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoreType
{
    Austri = 0,
    Norori,
    Vestri,
    Suori
}

public class Core : MonoBehaviour
{
    [SerializeField] private bool _purified;
    [SerializeField] private GameObject _purifiedObj, _unpurifiedObj;
    [SerializeField] private CoreType _coreType;
    [SerializeField] private List<GameObject> _corePrefabs;
    private GameObject _player;

    public bool Purified { get => _purified;}
    public CoreType CoreType { get => _coreType;}

    private void Update()
    {
        PurificationLogic();
    }

    public void SetCoreType(CoreType typeVal)
    {
        _coreType = typeVal;
    }

    private void PurificationLogic()
    {
        if (_purified && _unpurifiedObj)
        {
            _purifiedObj.SetActive(true);
            _unpurifiedObj.SetActive(false);
        }
        else if (!_purified && _unpurifiedObj)
        {
            _purifiedObj.SetActive(false);
            _unpurifiedObj.SetActive(true);
        }

        _unpurifiedObj = _corePrefabs[(int)_coreType];

        foreach (var item in _corePrefabs)
        {
            if (_unpurifiedObj != item)
            {
                item.SetActive(false);
            }
        }
    }

    public void PurifyCore()
    {
        _purified = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = null;
        }
    }
}
