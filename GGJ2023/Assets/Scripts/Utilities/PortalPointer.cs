using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPointer : MonoBehaviour
{
    [SerializeField] private GameObject _gFX;
    [SerializeField] private SpawnPortal _portalSpawner;
    [SerializeField] private Transform _spawnedPortal;
    [SerializeField] private bool _isSpawned;

    public bool IsSpawned { get => _isSpawned;}


    // Update is called once per frame
    void Update()
    {
        if (_portalSpawner.newPortal != null && !_isSpawned)
        {
            _spawnedPortal = _portalSpawner.newPortal.transform;
            _isSpawned = true;
        }

        if (_spawnedPortal == null && _isSpawned)
        {
            Destroy(gameObject);
        }

        if (_spawnedPortal != null && _isSpawned)
        {
            _gFX.SetActive(true);
            transform.LookAt(_spawnedPortal);
        }
        else
        {
            _gFX.SetActive(false);
        }
    }
}
