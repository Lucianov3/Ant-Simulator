using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larvae_Script : MonoBehaviour
{
    public static bool DontSpawnAtStart;

    private IEnumerator Bruht()
    {
        yield return new WaitForSeconds(5f);

        GameObject temp = GameManager.Ants.Dequeue();                                               //Ameise wird aus der Queue genommen

        if (Mathf.RoundToInt((GameManager.CurrentAnts * 20) / 100) <= GameManager.SoldatenInstanzen.Count + 1)
        {
            temp.AddComponent<AmeisenTypen.Arbeiter>();
            GameManager.CurrentAnts++;
            GameManager.ArbeiterInstanzen.Add(temp);
        }
        else
        {
            temp.AddComponent<AmeisenTypen.Soldat>();
            GameManager.CurrentAnts++;
            GameManager.SoldatenInstanzen.Add(temp);
        }
        temp.SetActive(true);

        temp.transform.SetPositionAndRotation(this.gameObject.transform.position + new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f));
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