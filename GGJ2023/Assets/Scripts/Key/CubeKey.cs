using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeKey : MonoBehaviour
{
    public static CubeKey Instance;

    [SerializeField] private Transform _holder;
    [SerializeField] private CoreType _coreType;

    public CoreType CoreType { get => _coreType;}

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
    }
    private void Update()
    {
        RotateToType();
    }
    private void OnValidate()
    {
        RotateToType();
    }

    [ContextMenu("NextCoreType")]
    public void NextCoreType()
    {
        if (_coreType == (CoreType)3)
        {
            _coreType = 0;
        }
        else
        {
            _coreType++;
        }

        //RotateToType();
    }

    private void RotateToType()
    {
        switch (_coreType)
        {
            case CoreType.Austri:
                _holder.localRotation = Quaternion.Euler( Vector3.Lerp(_holder.localRotation.eulerAngles, Vector3.zero,0.01f));
                //_holder.localRotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case CoreType.Norori:
                _holder.localRotation = Quaternion.Euler(Vector3.Lerp(_holder.localRotation.eulerAngles, new Vector3(0f, 90f, 0f), 0.2f));
                //_holder.localRotation = Quaternion.Euler(0f, 90f, 0f);
                break;
            case CoreType.Vestri:
                _holder.localRotation = Quaternion.Euler(Vector3.Lerp(_holder.localRotation.eulerAngles, new Vector3(0f, 180f, 0f), 0.2f));
                //_holder.localRotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            case CoreType.Suori:
                _holder.localRotation = Quaternion.Euler(Vector3.Lerp(_holder.localRotation.eulerAngles, new Vector3(0f, 270f, 0f), 0.2f));
                //_holder.localRotation = Quaternion.Euler(0f, 270f, 0f);
                break;
        }
    }


}
