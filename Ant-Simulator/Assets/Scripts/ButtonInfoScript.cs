using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfoScript : MonoBehaviour
{
    private bool isAvailible = true;
    private Text infoText;
    private float fade;

    [SerializeField]
    private GameObject button;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnMouseEnter(Collider other)
    {
        Debug.Log("berührt");
    }
}