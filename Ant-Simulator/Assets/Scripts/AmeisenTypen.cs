using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace AmeisenTypen
{
    public abstract class StandardAmeise : MonoBehaviour
    {
        protected string Name { get; set; }

        protected string Gender { get; set; }

        protected float Health { get; set; }

        protected float Thirsty { get; set; }
        protected GameObject Ameise { get; set; }

        public string RandomName()
        {
            if (Gender == "Männlich")
                Name = GameManager.NameListM[Random.Range(0, GameManager.NameListM.Count)];
            else
                Name = GameManager.NameListW[Random.Range(0, GameManager.NameListW.Count)];

            return Name;
        }

        public string RandomGender()
        {
            int temp = Random.Range(1, 3);
            if (temp == 1)
                Gender = "Männlich";
            else
                Gender = "Weiblich";
            return Gender;
        }
    }

    public class Arbeiter : StandardAmeise
    {
        public Arbeiter(GameObject Ameise)
        {
            Gender = RandomGender();
            Name = RandomName();
            Ameise.name = Name + "(Arbeiter)";
            this.Ameise = Ameise;
            Health = 100;
            Thirsty = 100;
        }

        //Hier musst du die logik für die arbeiter einfügen.
    }

    public class Soldat : StandardAmeise
    {
        public Soldat()
        {
        }
    }
}