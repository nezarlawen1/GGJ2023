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

    private void Awake()
    {
        playerInput = new PlayerInput();
        player = playerInput.Player;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        if (gameObject.CompareTag("Player"))
        {

        }

    }
    private void FixedUpdate()
    {
        motor.ProcessMove(player.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(player.Look.ReadValue<Vector2>());
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
