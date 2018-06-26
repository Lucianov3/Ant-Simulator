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
<<<<<<< HEAD
        public float MaxHealth { get; protected set; }
        public float Energy { get; protected set; }
        public float MaxEnergy { get; protected set; }
        public float Hunger { get; protected set; }
        public float MaxHunger { get; protected set; }
        public float Thirst { get; protected set; }
        public float MaxThirst { get; protected set; }
        public GameObject Ameise { get; protected set; }
=======
        public float Energy { get; protected set; }
        public float Hunger { get; protected set; }
        public float Thirsty { get; protected set; }
>>>>>>> 9b092f499ca28cb6a86743b3b84bef9703779ad8

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

        public float ReturnStat(string wantedStat)
        {
            switch (wantedStat)
            {
                case "Health":
                    return Health/MaxHealth;
                case "Energy":
                    return Energy/MaxEnergy;
                case "Hunger":
                    return Hunger/MaxHunger;
                case "Thirst":
                    return Thirst/MaxThirst;
                default:
                    return 0;
            }
        }
    }

    public class Arbeiter : StandardAmeise
    {
        private bool hungry;
        private bool thirsty;
        private bool theChosenOne;

        private void Start()
        {
<<<<<<< HEAD
            Gender = RandomGender();
            Name = RandomName();
            Ameise.name = Name + "(Arbeiter)";
            this.Ameise = Ameise;
            Health = 100;
            MaxHealth = Health;
            Hunger = 100;
            MaxHunger = Hunger;
            Energy = 100;
            MaxEnergy = Energy;
            Thirst = 100;
            MaxThirst = Thirst;
=======
            this.gameObject.name = Name + " " + Gender;
>>>>>>> 9b092f499ca28cb6a86743b3b84bef9703779ad8
        }

        //Hier musst du die logik für die arbeiter einfügen.
    }

    public class Soldat : StandardAmeise
    {
<<<<<<< HEAD
        public Soldat(GameObject Ameise)
        {
            Gender = RandomGender();
            Name = RandomName();
            Ameise.name = Name + "(Soldat)";
            this.Ameise = Ameise;
            Health = 100;
            MaxHealth = Health;
            Hunger = 100;
            MaxHunger = Hunger;
            Energy = 100;
            MaxEnergy = Energy;
            Thirst = 100;
            MaxThirst = Thirst;
        }
=======
        private bool hungry;
        private bool thirsty;
>>>>>>> 9b092f499ca28cb6a86743b3b84bef9703779ad8
    }
}