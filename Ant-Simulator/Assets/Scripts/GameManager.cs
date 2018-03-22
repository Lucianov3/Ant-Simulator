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

    private void Start()
    {
        antPool = GameObject.Find("AntPool");
        ant = GameObject.Find("operarius cibo formica");
        for (int i = 0; i < MaxAnts; i++)
        {
            GameObject temp = Instantiate(ant, antPool.transform);
            Ants.Enqueue(temp);
            temp.gameObject.SetActive(false);
        }
    }
}