using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    Camera overworldCamera;
    Camera underworldCamera;
    Ray ray;
    [SerializeField] LayerMask layerMask;

    void Start ()
    {
        overworldCamera = GameObject.Find("Overworld Camera").transform.GetChild(0).GetComponent<Camera>();
        //underworldCamera = GameObject.Find("Underworld Camera").GetComponent<Camera>();
    }

	void Update ()
    {
        if (overworldCamera.gameObject.activeSelf)
        {
            ray = overworldCamera.ScreenPointToRay(Input.mousePosition);
        }
        else
        {
            ray = underworldCamera.ScreenPointToRay(Input.mousePosition);
        }
        RaycastHit hitInfo;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray,out hitInfo, layerMask))
            {
                if (hitInfo.transform.GetComponent<AmeisenTypen.Arbeiter>())
                {
                    //Show Arbeiter Stats
                }
                else if(hitInfo.transform.GetComponent<AmeisenTypen.Soldat>())
                {
                    //Show Soldat Stats
                }
                else
                {
                    //Show Königin Stats
                }
            }
        }
    }
}
