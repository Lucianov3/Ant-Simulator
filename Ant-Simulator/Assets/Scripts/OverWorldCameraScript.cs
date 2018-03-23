using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCameraScript : MonoBehaviour
{
    Camera camera;
    GameObject parent;
    public float maxDistanceCameraToFloor = 10; 
    public float minDistanceCameraToFloor = 1;
    public float cameraMovementSpeed = 0.1f;
    public float cameraRotationSpeed = 1;
    public float zoomSpeed = 5;
    Vector3 cameraMovement;
    Vector3 cameraZoom;
    Vector3 tempMousePosition;
    bool rightMouseButtonIsBeingPressed;

    void Start ()
    {
        camera = GetComponent<Camera>();
        parent = camera.transform.parent.gameObject;
        transform.position = new Vector3(0,maxDistanceCameraToFloor, -maxDistanceCameraToFloor);
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
                parent.transform.Rotate(new Vector3(0,1,0), temp * cameraRotationSpeed, Space.Self);
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
            cameraZoom += new Vector3(0, -Input.GetAxis("Mouse ScrollWheel")*zoomSpeed, 0);
        }
        parent.transform.Translate(cameraMovement.normalized*cameraMovementSpeed*transform.position.y);
        transform.position += cameraZoom;
        if(transform.position.y>maxDistanceCameraToFloor)
        {
            transform.position = new Vector3(transform.position.x, maxDistanceCameraToFloor, transform.position.z);
        }
        if(transform.position.y < minDistanceCameraToFloor)
        {
            transform.position = new Vector3(transform.position.x, minDistanceCameraToFloor, transform.position.z);
        }
    }
}
