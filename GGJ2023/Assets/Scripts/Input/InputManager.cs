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

        if (gameObject.CompareTag("Player"))
        {
            player.SwapToScout.performed += ctx => ControllersSwapManager.Instance.SwapControlToScout(true);

            player.ShootPress.performed += ctx =>
            {
                if (ctx.interaction is PressInteraction)
                {
                    ShootPress = true;
                }
            };
            player.ShootPress.canceled += ctx => ShootPress = false;

            player.ShootHold.performed += ctx =>
            {
                if (ctx.interaction is HoldInteraction)
                {
                    ShootHold = true;
                }
            };
            player.ShootHold.canceled += ctx => ShootHold = false;
        }
    }
    private void FixedUpdate()
    {
        if (gameObject.CompareTag("Player"))
            motor.ProcessPlayerMove(player.Movement.ReadValue<Vector2>());
        else if(gameObject.CompareTag("Scout"))
            motor.ProcessScoutMove(player.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        if (gameObject.CompareTag("Player"))
            look.ProcessPlayerLook(player.Look.ReadValue<Vector2>());
        else if(gameObject.CompareTag("Scout"))
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
