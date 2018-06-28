using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static int MaxAnts = 98;                             //Wird mit den anfang ameisen addiert

    public static int CurrentAnts = 3;                           //Drei ameisen am anfang 1xKönigen,1xArbeiter,1xSoldat

    [SerializeField]
    public int CurrentAnts2 = 3;                           //Drei ameisen am anfang 1xKönigen,1xArbeiter,1xSoldat

    public static int StorageFood;
    private string fileline;
    private string fileline2;

    public static Queue<GameObject> Ants_Arbeiter = new Queue<GameObject>();
    public static Queue<GameObject> Ants_Soldaten = new Queue<GameObject>();
    public static List<string> NameListW = new List<string>();
    public static List<string> NameListM = new List<string>();
    public static List<GameObject> ArbeiterInstanzen = new List<GameObject>();
    public static List<GameObject> SoldatenInstanzen = new List<GameObject>();

    private GameObject ant;
    private GameObject Ant_Soldat;
    private GameObject antPool;
    public static GameObject overworldCamera;
    public static GameObject underworldCamera;

    private IEnumerator CheckForfood()
    {
        yield return new WaitForSeconds(360);
        if (StorageFood == 0)
        {
            GameObject temp = ArbeiterInstanzen[Random.Range(0, ArbeiterInstanzen.Count - 1)];
            temp.GetComponent<AmeisenTypen.Arbeiter>().getFood = true;
        }
        StartCoroutine(CheckForfood());
    }

    private void FixedUpdate()
    {
        CurrentAnts2 = CurrentAnts;
    }

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
        Ant_Soldat = GameObject.Find("Soldier");

        SoldatenInstanzen.Add(Ant_Soldat);
        ArbeiterInstanzen.Add(ant);
        if (antPool != null)
        {
            for (int i = 0; i <= Mathf.RoundToInt((MaxAnts * 80) / 100); i++)
            {
                GameObject temp = Instantiate(ant, antPool.transform);
                temp.transform.position = antPool.transform.position;
                Ants_Arbeiter.Enqueue(temp);
                temp.gameObject.SetActive(false);
                temp.name = "arbeiter" + i;
            }
            for (int i = 0; i <= Mathf.RoundToInt((MaxAnts * 20) / 100); i++)
            {
                GameObject temp = Instantiate(Ant_Soldat, antPool.transform);
                temp.transform.position = antPool.transform.position;
                Ants_Soldaten.Enqueue(temp);
                temp.gameObject.SetActive(false);
                temp.name = "Soldat" + i;
            }
        }
<<<<<<< HEAD
        Ant_Soldat.AddComponent<AmeisenTypen.Soldat>();
        ant.AddComponent<AmeisenTypen.Arbeiter>();
        StartCoroutine(CheckForfood());
=======

        //ant.AddComponent<AmeisenTypen.Arbeiter>();
>>>>>>> 2ba65e0b06e56cbdf0dc63379ac458ed62348dba
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