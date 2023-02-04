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

    public delegate void PlaySoundDelegate(SoundManager.SoundType soundType);
    public event PlaySoundDelegate PlaySoundEvent;

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
        SoundManager.Instance.PlaySound(SoundManager.SoundType.None);
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
            SoundManager.Instance.PlaySound(SoundManager.SoundType.TurnToLight);
            //if (PlaySoundEvent != null) PlaySoundEvent.Invoke(SoundManager.SoundType.TurnToLight);
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
            SoundManager.Instance.PlaySound(SoundManager.SoundType.TurnToHuman);
            //if (PlaySoundEvent != null) PlaySoundEvent.Invoke(SoundManager.SoundType.TurnToHuman);
        }
    }
}
