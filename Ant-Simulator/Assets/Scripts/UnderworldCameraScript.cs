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
        GameManager.underworldCamera = gameObject;
        camera = GetComponent<Camera>();
        transform.position = new Vector3(0,0, -MaxDistanceCameraToWall);
        gameObject.SetActive(false);
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
            cameraZoom += new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed);
        }

        transform.Translate(cameraMovement.normalized * CameraMovementSpeed * transform.position.z*Time.unscaledDeltaTime);
        transform.position += cameraZoom;

        if(transform.position.x >= 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }

        if (transform.position.y >= 18)
        {
            transform.position = new Vector3(transform.position.x, 18, transform.position.z);
        }
        if (transform.position.y <= -18)
        {
            transform.position = new Vector3(transform.position.x,-18, transform.position.z);
        }

        if (transform.position.z < -MaxDistanceCameraToWall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -MaxDistanceCameraToWall);
        }
        if (transform.position.z > -MinDistanceCameraToWall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -MinDistanceCameraToWall);
        }
    }
}
