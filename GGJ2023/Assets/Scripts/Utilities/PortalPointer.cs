using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPointer : MonoBehaviour
{
    [SerializeField] private Transform _portal;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_portal);
    }
}
