using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float zoom;
    public float speed;
    [Range(-1,1)]
    public float pitch;
    CharacterMotor motor;
    public LayerMask cameraMask;
    private float nextPitch, nextYaw;
	void Start () {
        InputController.instance.onAxisInput += CameraCalc;
        motor = FindObjectOfType<CharacterMotor>();
    }

    private void LateUpdate()
    {
        Vector3 currentOffset;
        currentOffset = transform.position - motor.transform.position;//Get offset vector between player and cam
        currentOffset = new Vector3(currentOffset.x, 0, currentOffset.z).normalized;//x and z offset mapped to unit circle
        float currentAngle = Mathf.Atan2(currentOffset.z, currentOffset.x);//get the current angle wich should be between 0 and PI
        currentAngle += nextYaw;//Add change in angle we want in radians
        float x = Mathf.Cos(currentAngle), y = Mathf.Sin(currentAngle);//Map the radian angle to a unit circle point
        //float cos = Mathf.Cos(currentAngle+ nextYaw);
        //float sin = Mathf.Sin(currentAngle + nextYaw);
        currentOffset = new Vector3(x, pitch, y) * zoom;//Apply pitch and zoom
        RaycastHit hit;
        if(Physics.Raycast(motor.transform.position, currentOffset.normalized, out hit, currentOffset.magnitude, cameraMask))
        {
            currentOffset = (currentOffset.normalized *hit.distance);
        }
        transform.position = motor.transform.position + currentOffset;
        transform.LookAt(motor.transform);
        nextYaw = 0;
    }

    void CameraCalc(string axis, float state) {
        if(axis == "CameraVertical")
        {
            pitch += state * speed;
        }
        if(axis == "CameraHorizontal")
        {
            nextYaw = -state * speed;
        }
    }
}
