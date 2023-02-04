using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance;
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
    public enum HudState
    {
        HumanState,
        ScoutState,
        HumanInMazeState
    }

    public HudState state;

    [SerializeField] private GameObject Sight;
    [SerializeField] private GameObject TeleportIn;
    [SerializeField] private GameObject RotateCube;
    [SerializeField] private GameObject Purify;
    [SerializeField] private GameObject TeleportOut;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case HudState.HumanState:
                Sight.SetActive(true);
                TeleportIn.SetActive(true);
                RotateCube.SetActive(true);
                Purify.SetActive(false);
                TeleportOut.SetActive(false);
                break;
            case HudState.ScoutState:
                Sight.SetActive(false);
                TeleportIn.SetActive(false);
                RotateCube.SetActive(false);
                Purify.SetActive(false);
                TeleportOut.SetActive(true);
                break;
            case HudState.HumanInMazeState:
                Sight.SetActive(false);
                TeleportIn.SetActive(false);
                RotateCube.SetActive(false);
                Purify.SetActive(true);
                TeleportOut.SetActive(true);
                break;
        }
    }
}
