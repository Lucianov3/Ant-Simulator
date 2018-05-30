using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCameraScript : MonoBehaviour
{
    Camera camera;
    GameObject parent;
    public float MaxDistanceCameraToFloor = 10; 
    public float MinDistanceCameraToFloor = 1;
    public float CameraMovementSpeed = 0.1f;
    public float CameraRotationSpeed = 1;
    public float ZoomSpeed = 5;
    Vector3 cameraMovement;
    Vector3 cameraZoom;
    Vector3 tempMousePosition;
    bool rightMouseButtonIsBeingPressed;

    void Start ()
    {
        camera = GetComponent<Camera>();
        parent = camera.transform.parent.gameObject;
        transform.position = new Vector3(0,MaxDistanceCameraToFloor, -MaxDistanceCameraToFloor);
	}
	
	void Update ()
    {
        cameraMovement = new Vector3(0, 0, 0);
        cameraZoom = new Vector3(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rightMouseButtonIsBeingPressed = true;
            tempMousePosition = camera.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (tempMousePosition != camera.ScreenToViewportPoint(Input.mousePosition))
            {
                float temp = tempMousePosition.x - camera.ScreenToViewportPoint(Input.mousePosition).x;
                parent.transform.Rotate(new Vector3(0,1,0), temp * CameraRotationSpeed, Space.Self);
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            rightMouseButtonIsBeingPressed = false;

        }
        if (Input.GetKey("w")&& !rightMouseButtonIsBeingPressed|| camera.ScreenToViewportPoint(Input.mousePosition).y >= 0.99f && !rightMouseButtonIsBeingPressed)
        {
            cameraMovement += Vector3.forward;
        }
        if (Input.GetKey("s") && !rightMouseButtonIsBeingPressed || camera.ScreenToViewportPoint(Input.mousePosition).y <= 0.01f && !rightMouseButtonIsBeingPressed)
        {
            cameraMovement += -Vector3.forward;
        }
        if (Input.GetKey("a") && !rightMouseButtonIsBeingPressed || camera.ScreenToViewportPoint(Input.mousePosition).x <= 0.01f && !rightMouseButtonIsBeingPressed)
        {
            cameraMovement += -Vector3.right;
        }
        if (Input.GetKey("d") && !rightMouseButtonIsBeingPressed || camera.ScreenToViewportPoint(Input.mousePosition).x >= 0.99f && !rightMouseButtonIsBeingPressed)
        {
            cameraMovement += Vector3.right;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !rightMouseButtonIsBeingPressed)
        {
            cameraZoom += new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed);
        }
        parent.transform.Translate(cameraMovement.normalized*CameraMovementSpeed*transform.position.y*Time.deltaTime);
        transform.Translate(cameraZoom*Time.deltaTime,Space.Self);
        if(transform.position.y > MaxDistanceCameraToFloor)
        {
            transform.position = new Vector3(transform.position.x, MaxDistanceCameraToFloor, transform.position.z);
        }
        if (transform.localPosition.z < -MaxDistanceCameraToFloor)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MaxDistanceCameraToFloor);
        }
        if (transform.position.y < MinDistanceCameraToFloor )
        {
            transform.position = new Vector3(transform.position.x, MinDistanceCameraToFloor, transform.position.z);
        }
        if (transform.localPosition.z > -MinDistanceCameraToFloor)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MinDistanceCameraToFloor);
        }
    }
}
