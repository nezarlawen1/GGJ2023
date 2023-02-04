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
    public LayerMask GroundLayer;
    #endregion
    #region Monobehaviour Callbacks
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gravity = Gravity;
        speed = movementSpeed;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Core"))
        {
            GameManager.Instance.CurrentCore = other.GetComponent<Core>();
        }
    }
    #endregion
    #region Methods
    public void ProcessPlayerMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * movementSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessScoutMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * movementSpeed * Time.deltaTime);
    }
    #endregion
}
