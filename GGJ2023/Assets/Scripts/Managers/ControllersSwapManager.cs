using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersSwapManager : MonoBehaviour
{
    private ControllersSwapManager _instance;

    public ControllersSwapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ControllersSwapManager();
            }
            return _instance;
        }
    }

    public InputManager PlayerInputManager;
    public InputManager ScoutInputManager;

    public GameObject PlayerCamera;
    public GameObject ScoutCamera;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       SwapControlToScout(false);
    }

    public void SwapControlToScout(bool state)
    {
        if (state)
        {
            PlayerInputManager.OnDisable();
            ScoutInputManager.OnEnable();
            PlayerCamera.SetActive(false);
            ScoutCamera.SetActive(true);
        }
        else
        {
            PlayerInputManager.OnEnable();
            ScoutInputManager.OnDisable();
            PlayerCamera.SetActive(true);
            ScoutCamera.SetActive(false);
        }
    }
}
