using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static int MaxAnts = 100;
    public static int CurrentAnts = 3;
    private string fileline;
    private string fileline2;

    public static Queue<GameObject> Ants = new Queue<GameObject>();
    public static List<string> NameListW = new List<string>();
    public static List<string> NameListM = new List<string>();
    public static List<AmeisenTypen.Arbeiter> ArbeiterInstanzen = new List<AmeisenTypen.Arbeiter>();
    public static List<AmeisenTypen.Soldat> SoldatenInstanzen = new List<AmeisenTypen.Soldat>();

    private GameObject ant;
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
        if (GameObject.Find("Overworld Camera"))
        {
            overworldCamera = GameObject.Find("Overworld Camera");
            underworldCamera = GameObject.Find("Underworld Camera");
            underworldCamera.SetActive(false);
        }
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