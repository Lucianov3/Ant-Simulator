using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

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

        public enum CurrentState
        { Bringing_Food, IsEating, fertilizes_the_queen, IsSleeping, Nothing, }

        public string RandomGender()
        {
            int temp = Random.Range(1, 3);
            if (temp == 1)
                Gender = "Male";
            else
                Gender = "Female";
            return Gender;
        }

        public IEnumerator Eat()
        {
            yield return new WaitForSeconds(10);                                                   //dauer wie lange sie essen.
        }

        public float ReturnStat(string wantedStat)
        {
            switch (wantedStat)
            {
                case "Health":
                    return Health / MaxHealth;

                case "Energy":
                    return Energy / MaxEnergy;

                case "Hunger":
                    return Hunger / MaxHunger;

                case "Thirst":
                    return Thirst / MaxThirst;

                default:
                    return 0;
            }
        }
    }

    public class Arbeiter : StandardAmeise
    {
        private NavMeshAgent antAgent;
        private GameObject queen;

        private Vector3 foodLocation;
        private Vector3 eatZone;
        private Vector3 queenLocation;

        public float hunger;

        private bool hungry;
        private bool thirsty;
        public bool TheChosenOne { get; set; }
        private bool getFood;

        [SerializeField]
        private CurrentState state;

        private void Start()
        {
            Gender = RandomGender();
            Name = RandomName() + " (arbeiter)";
            Health = 100;
            MaxHealth = Health;

            Hunger = 70;
            hunger = Hunger;
            MaxHunger = Hunger;
            Energy = 100;
            MaxEnergy = Energy;
            Thirst = 100;
            MaxThirst = Thirst;
            this.gameObject.name = Name + " " + Gender;
            antAgent = gameObject.GetComponent<NavMeshAgent>();
            state = CurrentState.Nothing;
            queen = GameObject.Find("Queen_New_Prefab");
            queenLocation = queen.gameObject.transform.position;
        }

        private void Update()
        {
            if (TheChosenOne)
            {
                state = CurrentState.fertilizes_the_queen;
            }

            switch (state)
            {
                case CurrentState.Bringing_Food:
                    GetFood();
                    break;

                case CurrentState.IsEating:
                    Eate();
                    break;

                case CurrentState.fertilizes_the_queen:
                    Fertilizes_the_queen();
                    break;

                case CurrentState.IsSleeping:                                                            //To Do
                    break;

                case CurrentState.Nothing:
                    antAgent.SetDestination(GameObject.Find("NothingLocation").transform.position);
                    break;

                default:
                    break;
            }
        }

        private void GetFood()
        {
            bool destinationSet = false;
            float dist = Vector3.Distance(gameObject.transform.position, foodLocation);

            if (!destinationSet)
            {
                antAgent.SetDestination(foodLocation);
                destinationSet = true;
            }

            if (dist <= 1)
            {
                //Pick Up Script
                destinationSet = false;
            }
        }                                                                       //ToDo!

        private void Eate()
        {
            bool destinationset = false;
            float dist = Vector3.Distance(gameObject.transform.position, eatZone);
            if (!destinationset)
            {
                antAgent.SetDestination(eatZone);
            }
            if (dist <= 1)
            {
                StartCoroutine(Eat());
                for (int i = 0; i < 2; i++)
                {
                    Hunger += Random.Range(20, 40);
                }
                if (Hunger > MaxHunger)
                {
                    Hunger = 100;
                }
                hungry = false;
            }
        }

        private void Fertilizes_the_queen()
        {
            TheChosenOne = false;
            bool destinationset = false;
            float dist = Vector3.Distance(gameObject.transform.position, queenLocation);

            if (!destinationset)
            {
                antAgent.SetDestination(queenLocation);
            }
            if (dist <= 2)
            {
                queen.GetComponent<KI_Rigina_formica>().SpawnLarva();
                antAgent.SetDestination(transform.position);
                state = CurrentState.Nothing;
            }
        }

        //Hier musst du die logik für die arbeiter einfügen.
    }

    public class Soldat : StandardAmeise
    {
        public void Start()
        {
            Gender = RandomGender();
            Name = RandomName() + " (Soldat)";
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

        private bool hungry;
        private bool thirsty;
        private bool onPartol;
    }
}