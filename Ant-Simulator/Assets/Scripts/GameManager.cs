using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static int MaxAnts = 97;                             //Wird mit den anfang ameisen addiert
    public static int CurrentAnts = 3;                          //Drei ameisen am anfang 1xKönigen,1xArbeiter,1xSoldat
    private string fileline;
    private string fileline2;

    public static Queue<GameObject> Ants = new Queue<GameObject>();
    public static List<string> NameListW = new List<string>();
    public static List<string> NameListM = new List<string>();
    public static List<GameObject> ArbeiterInstanzen = new List<GameObject>();
    public static List<GameObject> SoldatenInstanzen = new List<GameObject>();

    private GameObject ant;
    private GameObject Ant_Soldat;
    private GameObject antPool;
    public static GameObject overworldCamera;
    public static GameObject underworldCamera;

    private void Start()
    {
        StreamReader readerM = new StreamReader(Application.dataPath + @"\TextDateien\NamenM.txt");
        StreamReader readerW = new StreamReader(Application.dataPath + @"\TextDateien\NamenW.txt");

        while ((fileline = readerM.ReadLine()) != null)
        {
            NameListM.Add(readerM.ReadLine());
        }
        readerM.Close();

        while ((fileline2 = readerW.ReadLine()) != null)
        {
            NameListW.Add(readerW.ReadLine());
        }
        readerW.Close();

        antPool = GameObject.Find("AntPool");
        ant = GameObject.Find("Worker");

        ArbeiterInstanzen.Add(ant);
        if (antPool != null)
        {
            for (int i = 0; i <= MaxAnts; i++)
            {
                GameObject temp = Instantiate(ant, antPool.transform);
                temp.transform.position = antPool.transform.position;
                Ants.Enqueue(temp);
                temp.gameObject.SetActive(false);
                temp.name = "Ameise" + i;
            }
        }

        //ant.AddComponent<AmeisenTypen.Arbeiter>();
    }

    public static void SwitchToUnderWorldCamera()
    {
        if (overworldCamera.activeSelf)
        {
            overworldCamera.SetActive(false);
            underworldCamera.SetActive(true);
        }
    }

    public static void SwitchToOverWorldCamera()
    {
        if (underworldCamera.activeSelf)
        {
            underworldCamera.SetActive(false);
            overworldCamera.SetActive(true);
        }
    }
}