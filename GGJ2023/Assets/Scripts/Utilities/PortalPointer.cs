using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPointer : MonoBehaviour
{
    [SerializeField] private Transform _portal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_portal);
    }
}
