using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions player;

    private PlayerMotor motor;
    private PlayerLook look;

    public bool ShootPress = false;
    public bool ShootHold = false;

    private void Awake()
    {
        playerInput = new PlayerInput();
        player = playerInput.Player;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        player.CycleKey.performed += ctx => CubeKey.Instance.NextCoreType();

    }
    private void Update()
    {
        player.SwapToScout.performed += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                if (gameObject.CompareTag("Player"))
                {
                    if (GameManager.Instance.CurrentMaze.IsPlayerInMaze)
                    {
                        gameObject.transform.position = ControllersSwapManager.Instance.CurrentPortalPos.position;
                        GameManager.Instance.CurrentMaze.IsPlayerInMaze = false;
                        GameManager.Instance.CurrentMaze.SwitchVision(false);
                    }
                    else
                    {
                        ControllersSwapManager.Instance.SwapControlToScout(true);
                    }
                }
                else if (gameObject.CompareTag("Scout"))
                {
                    ControllersSwapManager.Instance.SwapControlToScout(false);
                }
            }
        };
        player.Enteract.started += ctx =>
        {
            if (ctx.interaction is PressInteraction)
            {
                if (gameObject.CompareTag("Player"))
                {
                    if (!GameManager.Instance.CurrentMaze.IsPlayerInMaze && ControllersSwapManager.Instance.CurrentPortalPos != null)
                    {
                        //if player has the right key then enter key
                        if (GameManager.Instance.CurrentMaze._coreRef.GetComponent<Core>().CoreType == CubeKey.Instance.CoreType)
                        {
                            ControllersSwapManager.Instance.CurrentPortalPos.gameObject.GetComponent<PortalTeleporter>().open = true;
                        }
                        else
                        {
                            GameManager.Instance.CurrentMaze.RerollType();
                        }
                        if (ControllersSwapManager.Instance.CurrentPortalPos.gameObject.GetComponent<PortalTeleporter>().open)
                        {
                            ControllersSwapManager.Instance.PlayerCanTeleport = true;
                        }
                    }
                    if (GameManager.Instance.CurrentCore != null)
                    {
                        if (!GameManager.Instance.CurrentCore.Purified)
                        {
                            GameManager.Instance.CurrentCore.PurifyCore();
                            GameManager.Instance.CurrentMaze.SwitchVision(false);
                        }
                    }
                }
            }
        };
    }
    private void FixedUpdate()
    {
        if (gameObject.CompareTag("Player"))
            motor.ProcessPlayerMove(player.Movement.ReadValue<Vector2>());
        else if (gameObject.CompareTag("Scout"))
            motor.ProcessScoutMove(player.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        if (gameObject.CompareTag("Player"))
            look.ProcessPlayerLook(player.Look.ReadValue<Vector2>());
        else if (gameObject.CompareTag("Scout"))
            look.ProcessScoutLook(player.Look.ReadValue<Vector2>());
    }
    public void OnEnable()
    {
        player.Enable();
    }
    public void OnDisable()
    {
        player.Disable();
    }
}
