using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersSwapManager : MonoBehaviour
{
    public InputManager PlayerInputManager;
    public InputManager ScoutInputManager;

    public GameObject PlayerCamera;
    public GameObject ScoutCamera;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInputManager.OnEnable();
        ScoutInputManager.OnDisable();
    }

    public void SwapControlToScout(bool state)
    {
        if (state)
        {
            PlayerInputManager.OnDisable();
            ScoutInputManager.OnEnable();
        }
        else
        {
            PlayerInputManager.OnEnable();
            ScoutInputManager.OnDisable();
        }
    }
}
