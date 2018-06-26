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
        public float MaxHealth { get; protected set; }
        public float Energy { get; protected set; }
        public float MaxEnergy { get; protected set; }
        public float Hunger { get; protected set; }
        public float MaxHunger { get; protected set; }
        public float Thirst { get; protected set; }
        public float MaxThirst { get; protected set; }

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
            Gender = RandomGender();
            Name = RandomName();
            Health = 100;
            MaxHealth = Health;
            Hunger = 100;
            MaxHunger = Hunger;
            Energy = 100;
            MaxEnergy = Energy;
            Thirst = 100;
            MaxThirst = Thirst;
            this.gameObject.name = Name + " " + Gender;
        }

        //Hier musst du die logik für die arbeiter einfügen.
    }

    public class Soldat : StandardAmeise
    {
        public void Start()
        {
            Gender = RandomGender();
            Name = RandomName();
            Health = 100;
            MaxHealth = Health;
            Hunger = 100;
            MaxHunger = Hunger;
            Energy = 100;
            MaxEnergy = Energy;
            Thirst = 100;
            MaxThirst = Thirst;
        }
        private bool hungry;
        private bool thirsty;
    }
}