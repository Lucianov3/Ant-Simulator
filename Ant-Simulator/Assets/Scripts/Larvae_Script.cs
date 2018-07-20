using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larvae_Script : MonoBehaviour
{
    public static bool DontSpawnAtStart;

    private IEnumerator Bruht()
    {
        yield return new WaitForSeconds(180);

        if (Mathf.RoundToInt((GameManager.CurrentAnts * 20) / 100) <= GameManager.SoldatenInstanzen.Count)
        {
            GameObject temp1 = GameManager.Ants_Arbeiter.Dequeue();
            temp1.AddComponent<AmeisenTypen.Arbeiter>();
            GameManager.CurrentAnts++;
            GameManager.ArbeiterInstanzen.Add(temp1);
            temp1.transform.SetPositionAndRotation(this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f));
            temp1.SetActive(true);
        }
        else
        {
            GameObject temp2 = GameManager.Ants_Soldaten.Dequeue();                                               //Ameise wird aus der Queue genommen
            temp2.AddComponent<AmeisenTypen.Soldat>();
            GameManager.CurrentAnts++;
            GameManager.SoldatenInstanzen.Add(temp2);
            temp2.transform.SetPositionAndRotation(this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f));
            temp2.SetActive(true);
        }

        KI_Rigina_formica.LarvaeQ.Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (DontSpawnAtStart)
        {
            StartCoroutine(Bruht());
        }
    }
}