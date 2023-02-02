using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region Fields
    //Private Fields
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float speed = 5f;
    private float gravity;

    //Public Fields
    public float movementSpeed = 5f;
    public float Gravity = -9.8f;
    public Vector3 moveDirection;
    #endregion
    #region Monobehaviour Callbacks
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = Gravity;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    #endregion
    #region Methods
    public void ProcessMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    #endregion
}
