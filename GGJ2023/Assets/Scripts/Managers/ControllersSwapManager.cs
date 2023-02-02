using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersSwapManager : MonoBehaviour
{
    public static ControllersSwapManager Instance;
    

    public InputManager PlayerInputManager;
    public InputManager ScoutInputManager;

    public GameObject PlayerCamera;
    public GameObject ScoutCamera;

    public GameObject PlayerCanvases;
    public GameObject ScoutCanvases;

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
            PlayerCanvases.SetActive(false);
            ScoutCanvases.SetActive(true);
        }
        else
        {
            PlayerInputManager.OnEnable();
            ScoutInputManager.OnDisable();
            PlayerCamera.SetActive(true);
            ScoutCamera.SetActive(false);
            PlayerCanvases.SetActive(true);
            ScoutCanvases.SetActive(false);
        }
    }
}
