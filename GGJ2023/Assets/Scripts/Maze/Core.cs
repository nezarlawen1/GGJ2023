using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private bool _purified;
    [SerializeField] private GameObject _purifiedObj, _unpurifiedObj;
    private GameObject _player;

    public bool Purified { get => _purified; set => _purified = value; }


    private void Update()
    {
        PurificationLogic();
    }


    private void PurificationLogic()
    {
        if (_purified)
        {
            _purifiedObj.SetActive(true);
            _unpurifiedObj.SetActive(false);
        }
        else
        {
            _purifiedObj.SetActive(false);
            _unpurifiedObj.SetActive(true);
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
