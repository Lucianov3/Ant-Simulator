using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSemenScript : MonoBehaviour
{
    bool drop = true;
    [SerializeField] float dropSpeed;
    [SerializeField] LayerMask layerMask;
    Vector3 targetPosition;
    

    private void Start()
    {
        Ray tempRay = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        Physics.Raycast(tempRay,out hitInfo, Mathf.Infinity, layerMask);
        targetPosition = hitInfo.point;
    }

    private void Update()
    {
        if (drop)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition,dropSpeed);
        }
    }
}
