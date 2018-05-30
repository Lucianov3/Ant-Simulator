using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject ant;

    private GameObject antPool;
    public static int MaxAnts = 30;
    public static int CurrentAnts;

    public static Queue<GameObject> Ants = new Queue<GameObject>();

    public static GameObject overworldCamera;
    public static GameObject underworldCamera;

    private void Start()
    {
        antPool = GameObject.Find("AntPool");
        ant = GameObject.Find("operarius cibo formica");
        if (antPool != null)
        {
            for (int i = 0; i < MaxAnts; i++)
            {
                GameObject temp = Instantiate(ant, antPool.transform);
                Ants.Enqueue(temp);
                temp.gameObject.SetActive(false);
            }
        }
        if (GameObject.Find("Overworld Camera"))
        {
            overworldCamera = GameObject.Find("Overworld Camera");
            underworldCamera = GameObject.Find("Underworld Camera");
            underworldCamera.SetActive(false);

        }
    }

    public static void SwitchCamera()
    {
        if (overworldCamera.activeSelf)
        {
            overworldCamera.SetActive(false);
            underworldCamera.SetActive(true);
        }
        else
        {
            underworldCamera.SetActive(false);
            overworldCamera.SetActive(true);
        }
    }
}