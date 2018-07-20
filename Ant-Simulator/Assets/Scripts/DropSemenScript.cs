using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSemenScript : MonoBehaviour
{
    private bool drop = true;
    [SerializeField] private float dropSpeed;
    [SerializeField] private LayerMask layerMask;
    private Vector3 targetPosition;
    private RaycastHit hitInfo;

    private void Start()
    {
        Ray tempRay = new Ray(transform.position, Vector3.down);
        Physics.Raycast(tempRay, out hitInfo, Mathf.Infinity, layerMask);
        targetPosition = hitInfo.point;
    }

    private void Update()
    {
        if (drop)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, dropSpeed);
            if (Vector3.Distance(hitInfo.point, transform.position) <= 1)
            {
                drop = false;
            }
        }
    }
}