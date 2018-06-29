using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCameraScript : MonoBehaviour
{
    Camera camera;
    GameObject parent;
    public float MaxDistanceCameraToFloor = 10; 
    public float MinDistanceCameraToFloor = 1;
    public float CurrentDistanceCameraToFloor = 10;
    public float CameraMovementSpeed = 0.1f;
    public float CameraRotationSpeed = 1;
    public float ZoomSpeed = 5;
    public float MaxXPositionValue;
    public float MaxZPositionValue;
    Vector3 cameraMovement;
    Vector3 tempMousePosition;
    bool rightMouseButtonIsBeingPressed;
    public float RaycastDistance;
    public LayerMask LM;
    private float cameraResetSmoothness = 0.5f;

    public bool MouseMovementEnabled = true;

    void Start ()
    {
        GameManager.overworldCamera = transform.parent.gameObject;
        camera = GetComponent<Camera>();
        parent = camera.transform.parent.gameObject;
        transform.position = new Vector3(0,MaxDistanceCameraToFloor, 0);
	}
	
	void Update ()
    {
        cameraMovement = new Vector3(0, 0, 0);
        
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
            CurrentDistanceCameraToFloor -= Input.GetAxis("Mouse ScrollWheel")*ZoomSpeed;
            CurrentDistanceCameraToFloor = Mathf.Max(CurrentDistanceCameraToFloor, MinDistanceCameraToFloor);
            CurrentDistanceCameraToFloor = Mathf.Min(CurrentDistanceCameraToFloor, MaxDistanceCameraToFloor);
        }

        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 50);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, 50, LM);
        RaycastDistance = hitInfo.distance;
        //parent.transform.position = Vector3.Lerp(parent.transform.position, new Vector3(parent.transform.position.x, hitInfo.point.y, parent.transform.position.z), 0.1f);

        transform.localPosition = new Vector3(0, CurrentDistanceCameraToFloor, -CurrentDistanceCameraToFloor);
        parent.transform.Translate(cameraMovement.normalized*CameraMovementSpeed*transform.position.y*Time.unscaledDeltaTime);
        if(parent.transform.position.x >= MaxXPositionValue)
        {
            parent.transform.position = Vector3.Lerp(parent.transform.position, new Vector3(MaxXPositionValue, parent.transform.position.y, parent.transform.position.z), cameraResetSmoothness);
        }
        if (parent.transform.position.x <= -MaxXPositionValue)
        {
            parent.transform.position = Vector3.Lerp(parent.transform.position, new Vector3(-MaxXPositionValue, parent.transform.position.y, parent.transform.position.z), cameraResetSmoothness);
        }
        if (parent.transform.position.z >= MaxZPositionValue)
        {
            parent.transform.position = Vector3.Lerp(parent.transform.position, new Vector3(parent.transform.position.x, parent.transform.position.y, MaxZPositionValue), cameraResetSmoothness);
        }
        if (parent.transform.position.z <= -MaxZPositionValue)
        {
            parent.transform.position = Vector3.Lerp(parent.transform.position, new Vector3(parent.transform.position.x, parent.transform.position.y, -MaxZPositionValue), cameraResetSmoothness);
        }

        if (transform.localPosition.y > MaxDistanceCameraToFloor)
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
