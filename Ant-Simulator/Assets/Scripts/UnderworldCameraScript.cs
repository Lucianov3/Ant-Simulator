using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldCameraScript : MonoBehaviour
{
    Camera camera;
    public float maxDistanceCameraToFloor = 10;
    public float minDistanceCameraToFloor = 1;
    public float cameraMovementSpeed = 0.1f;
    public float zoomSpeed = 5;
    Vector3 cameraMovement;
    Vector3 cameraZoom;

    void Start ()
    {
        camera = GetComponent<Camera>();
        transform.position = new Vector3(-maxDistanceCameraToFloor, -maxDistanceCameraToFloor, 0);
	}

	void Update ()
    {
        cameraMovement = new Vector3(0, 0, 0);
        cameraZoom = new Vector3(0, 0, 0);
        if (Input.GetKey("w") || camera.ScreenToViewportPoint(Input.mousePosition).y >= 0.99f)
        {
            cameraMovement += -Vector3.up;
        }
        if (Input.GetKey("s") || camera.ScreenToViewportPoint(Input.mousePosition).y <= 0.01f)
        {
            cameraMovement += Vector3.up;
        }
        if (Input.GetKey("a") || camera.ScreenToViewportPoint(Input.mousePosition).x <= 0.01f)
        {
            cameraMovement += Vector3.right;
        }
        if (Input.GetKey("d") || camera.ScreenToViewportPoint(Input.mousePosition).x >= 0.99f)
        {
            cameraMovement += -Vector3.right;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraZoom += new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 0, 0);
        }
        transform.Translate(cameraMovement.normalized * cameraMovementSpeed * transform.position.y);
        transform.position += cameraZoom;
        if (transform.position.x < -maxDistanceCameraToFloor)
        {
            transform.position = new Vector3(-maxDistanceCameraToFloor, transform.position.y, transform.position.z);
        }
        if (transform.position.x > -minDistanceCameraToFloor)
        {
            transform.position = new Vector3(-minDistanceCameraToFloor, transform.position.y, transform.position.z);
        }
    }
}
