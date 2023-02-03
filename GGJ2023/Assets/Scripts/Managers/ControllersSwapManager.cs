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

    public GameObject ScoutRef;
    public Vector3 ScoutFirstPos;
    public Transform CurrentPortalPos;

    public bool PlayerInPortalCollider = false;
    public bool PlayerCanTeleport = false;

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
        ScoutFirstPos = ScoutRef.transform.position;
        SwapControlToScout(false);
    }

    public void SwapControlToScout(bool state)
    {
        if (state)
        {
            ScoutFirstPos = ScoutRef.transform.position;
            PlayerInputManager.OnDisable();
            ScoutInputManager.OnEnable();
            PlayerCamera.SetActive(false);
            ScoutCamera.SetActive(true);
            PlayerCanvases.SetActive(false);
            ScoutCanvases.SetActive(true);
            ScoutRef.SetActive(true);
        }
        else
        {
            ScoutRef.transform.position = ScoutFirstPos;
            ScoutRef.SetActive(false);
            PlayerInputManager.OnEnable();
            ScoutInputManager.OnDisable();
            PlayerCamera.SetActive(true);
            ScoutCamera.SetActive(false);
            PlayerCanvases.SetActive(true);
            ScoutCanvases.SetActive(false);
        }
    }
}
