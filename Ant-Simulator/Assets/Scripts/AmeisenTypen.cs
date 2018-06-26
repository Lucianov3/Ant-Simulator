using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AmeisenTypen
{
    public abstract class StandardAmeise : MonoBehaviour
    {
        public string Name { get; protected set; }

        public string Gender { get; protected set; }

        public float Health { get; protected set; }
        public float Energy { get; protected set; }
        public float Hunger { get; protected set; }
        public float Thirsty { get; protected set; }

        public string RandomName()
        {
            if (Gender == "Male")
                Name = GameManager.NameListM[Random.Range(0, GameManager.NameListM.Count)];
            else
                Name = GameManager.NameListW[Random.Range(0, GameManager.NameListW.Count)];

            return Name;
        }

        public string RandomGender()
        {
            int temp = Random.Range(1, 3);
            if (temp == 1)
                Gender = "Male";
            else
                Gender = "Female";
            return Gender;
        }
    }

    public class Arbeiter : StandardAmeise
    {
        private bool hungry;
        private bool thirsty;
        private bool theChosenOne;

        private void Start()
        {
            this.gameObject.name = Name + " " + Gender;
        }

        //Hier musst du die logik für die arbeiter einfügen.
    }

    public class Soldat : StandardAmeise
    {
        private bool hungry;
        private bool thirsty;
    }
}