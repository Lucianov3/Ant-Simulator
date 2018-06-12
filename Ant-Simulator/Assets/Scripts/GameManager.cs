using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static int MaxAnts = 30;
    public static int CurrentAnts;
    private string fileline;
    private string fileline2;

    public static Queue<GameObject> Ants = new Queue<GameObject>();
    public static List<string> NameListW = new List<string>();
    public static List<string> NameListM = new List<string>();

    private GameObject ant;
    private GameObject antPool;
    public static GameObject overworldCamera;
    public static GameObject underworldCamera;

    private StreamReader readerM = new StreamReader(@"C:\Users\Robert\Documents\GitHub\Ant-Simulator\Ant-Simulator\Assets\TextDateien\NamenM.txt");
    private StreamReader readerW = new StreamReader(@"C:\Users\Robert\Documents\GitHub\Ant-Simulator\Ant-Simulator\Assets\TextDateien\NamenW.txt");

    private void Start()
    {
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
        Debug.Log("hallo");
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