using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform CamHolder;
    public Camera cam;
    private float xRotaton = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calculate camera rotation for looking up and down
        xRotaton -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotaton = Mathf.Clamp(xRotaton, -80f, 80f);
        //apply this to the camera trasform
        //cam.transform.localRotation = Quaternion.Euler(xRotaton, 0f, 0f);
        CamHolder.localRotation = Quaternion.Euler(xRotaton, 0f, 0f);
        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
