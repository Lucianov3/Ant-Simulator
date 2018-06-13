using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Larvae_Script : MonoBehaviour
{
    public static bool DontSpawnAtStart;

    private IEnumerator Bruht()
    {
        yield return new WaitForSeconds(5f);
        GameObject temp = GameManager.Ants.Dequeue();
        if ((GameManager.CurrentAnts * 0.2) > GameManager.SoldatenInstanzen.Count)
        {
            AmeisenTypen.Soldat Ameise = new AmeisenTypen.Soldat(temp);
            GameManager.SoldatenInstanzen.Add(Ameise);
        }
        else
        {
            AmeisenTypen.Arbeiter Ameise = new AmeisenTypen.Arbeiter(temp);
            GameManager.ArbeiterInstanzen.Add(Ameise);
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