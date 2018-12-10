using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour {

    private CharacterController charController;
    public float speed;
    public float gravity;
    public float jumpForce;
    private float verticalVelocity = 0;
    private CameraController Cam;
    Vector3 movement = Vector3.zero;

    void Start () {
        InputController.instance.onButtonInput += ButtonCalc;
        InputController.instance.onAxisInput += MovementCalc;
        Cam = FindObjectOfType<CameraController>();
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        charController.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));
        if(charController.isGrounded)
        {
            verticalVelocity = 0;
        } else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        charController.Move(movement.normalized * speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(movement, Vector3.up);
        movement = Vector3.zero;
    }

    private void ButtonCalc(string button, InputState state)
    {
        Debug.Log(button+" "+ charController.isGrounded);
        if(button == "Jump")
        {
            if(charController.isGrounded)
            {
                verticalVelocity = jumpForce;
            }
        }
    }
    
    private void MovementCalc(string axis, float state) {
        if(axis == "Vertical")
        {
            movement += new Vector3(Cam.transform.forward.x, 0, Cam.transform.forward.z).normalized * -state;
        }
        if(axis == "Horizontal")
        {
            movement += new Vector3(Cam.transform.right.x, 0, Cam.transform.right.z).normalized * state;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (movement * 10));
    }
}
