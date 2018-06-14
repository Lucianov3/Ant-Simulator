using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCameraScript : MonoBehaviour
{
    Camera camera;
    GameObject parent;
    public float MaxDistanceCameraToFloor = 10; 
    public float MinDistanceCameraToFloor = 1;
    public float CurrentDisctanceCameraToFloor = 10;
    public float CameraMovementSpeed = 0.1f;
    public float CameraRotationSpeed = 1;
    public float ZoomSpeed = 5;
    Vector3 cameraMovement;
    Vector3 cameraZoom;
    Vector3 tempMousePosition;
    bool rightMouseButtonIsBeingPressed;
    public float RaycastDistance;
    public LayerMask LM;

    public bool MouseMovementEnabled = true;

    void Start ()
    {
        camera = GetComponent<Camera>();
        parent = camera.transform.parent.gameObject;
        transform.position = new Vector3(0,MaxDistanceCameraToFloor, 0);
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
        if (Input.GetKey("w")&& !rightMouseButtonIsBeingPressed|| camera.ScreenToViewportPoint(Input.mousePosition).y >= 0.99f && !rightMouseButtonIsBeingPressed && MouseMovementEnabled)
        {
            cameraMovement += Vector3.forward;
        }
        if (Input.GetKey("s") && !rightMouseButtonIsBeingPressed || camera.ScreenToViewportPoint(Input.mousePosition).y <= 0.01f && !rightMouseButtonIsBeingPressed && MouseMovementEnabled)
        {
            cameraMovement += -Vector3.forward;
        }
        if (Input.GetKey("a") && !rightMouseButtonIsBeingPressed || camera.ScreenToViewportPoint(Input.mousePosition).x <= 0.01f && !rightMouseButtonIsBeingPressed && MouseMovementEnabled)
        {
            cameraMovement += -Vector3.right;
        }
        if (Input.GetKey("d") && !rightMouseButtonIsBeingPressed || camera.ScreenToViewportPoint(Input.mousePosition).x >= 0.99f && !rightMouseButtonIsBeingPressed && MouseMovementEnabled)
        {
            cameraMovement += Vector3.right;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !rightMouseButtonIsBeingPressed)
        {
            cameraZoom += new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed);
            CurrentDisctanceCameraToFloor -= Input.GetAxis("Mouse ScrollWheel")*ZoomSpeed;
            CurrentDisctanceCameraToFloor = Mathf.Max(CurrentDisctanceCameraToFloor, MinDistanceCameraToFloor);
            CurrentDisctanceCameraToFloor = Mathf.Min(CurrentDisctanceCameraToFloor, MaxDistanceCameraToFloor);
        }
        Ray ray = new Ray(transform.position, new Vector3(0,-150,150));
        Debug.DrawRay(transform.position, new Vector3(0, -150, 150));
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, 150, LM);
        RaycastDistance = hitInfo.distance;
        //transform.Translate(cameraZoom.normalized * ZoomSpeed * Time.unscaledDeltaTime);

        transform.localPosition = new Vector3(0, CurrentDisctanceCameraToFloor, -CurrentDisctanceCameraToFloor);
        //if(parent.transform.position.y - hitInfo.point.y >)
        parent.transform.position = new Vector3(parent.transform.position.x,hitInfo.point.y,parent.transform.position.z);

        parent.transform.Translate(cameraMovement.normalized*CameraMovementSpeed*transform.position.y*Time.unscaledDeltaTime);
        if(transform.localPosition.y > MaxDistanceCameraToFloor)
        {
            transform.localPosition = new Vector3(transform.position.x, MaxDistanceCameraToFloor, transform.position.z);
        }
        if (transform.localPosition.z < -MaxDistanceCameraToFloor)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MaxDistanceCameraToFloor);
        }
        if (transform.localPosition.y < MinDistanceCameraToFloor )
        {
            transform.localPosition = new Vector3(transform.position.x, MinDistanceCameraToFloor, transform.position.z);
        }
        if (transform.localPosition.z > -MinDistanceCameraToFloor)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MinDistanceCameraToFloor);
        }
    }
}
