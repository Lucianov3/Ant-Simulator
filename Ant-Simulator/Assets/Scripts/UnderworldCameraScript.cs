using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldCameraScript : MonoBehaviour
{
    Camera camera;
    public float MaxDistanceCameraToWall = 10;
    public float MinDistanceCameraToWall = 1;
    public float CameraMovementSpeed = 0.1f;
    public float ZoomSpeed = 5;
    Vector3 cameraMovement;
    Vector3 cameraZoom;

    public bool MouseMovementEnabled = true;


    void Start ()
    {
        camera = GetComponent<Camera>();
        transform.position = new Vector3(-MaxDistanceCameraToWall, -MaxDistanceCameraToWall, 0);
	}

	void Update ()
    {
        cameraMovement = new Vector3(0, 0, 0);
        cameraZoom = new Vector3(0, 0, 0);
        if (Input.GetKey("w") || camera.ScreenToViewportPoint(Input.mousePosition).y >= 0.99f && MouseMovementEnabled)
        {
            cameraMovement += -Vector3.up;
        }
        if (Input.GetKey("s") || camera.ScreenToViewportPoint(Input.mousePosition).y <= 0.01f && MouseMovementEnabled)
        {
            cameraMovement += Vector3.up;
        }
        if (Input.GetKey("a") || camera.ScreenToViewportPoint(Input.mousePosition).x <= 0.01f && MouseMovementEnabled)
        {
            cameraMovement += Vector3.right;
        }
        if (Input.GetKey("d") || camera.ScreenToViewportPoint(Input.mousePosition).x >= 0.99f && MouseMovementEnabled)
        {
            cameraMovement += -Vector3.right;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraZoom += new Vector3(Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed, 0, 0);
        }
        transform.Translate(cameraMovement.normalized * CameraMovementSpeed * transform.position.x*Time.unscaledDeltaTime);
        transform.position += cameraZoom;
        if (transform.position.x < -MaxDistanceCameraToWall)
        {
            transform.position = new Vector3(-MaxDistanceCameraToWall, transform.position.y, transform.position.z);
        }
        if (transform.position.x > -MinDistanceCameraToWall)
        {
            transform.position = new Vector3(-MinDistanceCameraToWall, transform.position.y, transform.position.z);
        }
    }
}
