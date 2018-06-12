using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AmeisenTypen
{
    public abstract class StandardAmeise : MonoBehaviour
    {
        private string name;
        private string gender;
        public float Health = 100;
        public float Hunger = 100;
        public float Thirsty = 100;

        private void RandomName()
        {
            if (gender == "Männlich")
                name = GameManager.NameListM[Random.Range(0, GameManager.NameListM.Count)];
            else
                name = GameManager.NameListM[Random.Range(0, GameManager.NameListW.Count)];
        }

        private void RandomGender()
        {
            int temp = Random.Range(1, 3);
            if (temp == 1)
                gender = "Männlich";
            else
                gender = "Weiblich";
        }
    }

    public class Arbeiter : StandardAmeise
    {
        public Arbeiter()
        {
        }
    }

    public class Soldat : StandardAmeise
    {
        public Soldat()
        {
        }
    }

    public class Larve : StandardAmeise
    {
        public Larve()
        {
        }
    }
}