using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KI_Rigina_formica : MonoBehaviour
{
    public GameObject Larvae;
    private bool isTheSpawnRateIncreased;

    public static Queue<GameObject> LarvaeQ = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < Larvae.transform.childCount; i++)                                          //Sucht Childs und Enqueue sie in die LarvaeQ
        {
            LarvaeQ.Enqueue(Larvae.transform.GetChild(i).gameObject);
            Larvae.transform.GetChild(i).gameObject.SetActive(false);
        }
        InvokeRepeating("Fruchtbarkeit", 20, 180);
    }

    private void FixedUpdate()
    {
        if (GameManager.CurrentAnts >= 15 && !isTheSpawnRateIncreased)
        {
            CancelInvoke();
            InvokeRepeating("Fruchtbarkeit", 0, 60);
            isTheSpawnRateIncreased = true;
        }
        else if (GameManager.CurrentAnts < 15 && isTheSpawnRateIncreased)
        {
            CancelInvoke();
            InvokeRepeating("Fruchtbarkeit", 0, 180);
            isTheSpawnRateIncreased = false;
        }
    }

    private void Fruchtbarkeit()
    {
        if (GameManager.CurrentAnts < 100)
        {
            GameObject temp;
            do
            {
                int i = Random.Range(0, GameManager.ArbeiterInstanzen.Count - 1);
                temp = GameManager.ArbeiterInstanzen[i];
            } while (temp.GetComponent<AmeisenTypen.Arbeiter>().Gender != "Male" && temp.GetComponent<AmeisenTypen.Arbeiter>().Energy != 0);
            temp.GetComponent<AmeisenTypen.Arbeiter>().TheChosenOne = true;
            Debug.Log("Fertilizes");
        }
    }

    public void SpawnLarva()
    {
        if (GameManager.CurrentAnts <= GameManager.MaxAnts && LarvaeQ.Count > 0)
        {                                                                                              //Holt eine Larve aus der Queue und setzt sie
            GameObject temp = LarvaeQ.Dequeue();                                                       //Active
            Larvae_Script.DontSpawnAtStart = true;
            temp.gameObject.SetActive(true);
        }
    }
}