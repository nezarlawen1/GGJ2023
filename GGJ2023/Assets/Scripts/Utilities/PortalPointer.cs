using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPointer : MonoBehaviour
{
    [SerializeField] private Transform _portal;

    // Update is called once per frame
    void Update()
    {
        if (_portal == null)
        {
            Destroy(gameObject);
        }
        transform.LookAt(_portal);
    }
}
