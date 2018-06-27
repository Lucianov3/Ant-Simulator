using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAntClass : MonoBehaviour
{
	void Start ()
    {
        gameObject.AddComponent<AmeisenTypen.Arbeiter>();
	}
}
