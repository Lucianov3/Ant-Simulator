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

        public string Gender { get; set; }

        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }
        public float Energy { get; protected set; }
        public float MaxEnergy { get; protected set; }
        public float Age { get; set; }
        public float DeathMark { get; set; }

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
        { Bringing_Food, IsEating, fertilizes_the_queen, IsSleeping, NothingToDo, Waiting, Dead }

        public string RandomGender()
        {
            int temp = Random.Range(1, 3);
            if (temp == 1)
                Gender = "Male";
            else
                Gender = "Female";
            return Gender;
        }

        public float GenerateDethMark()
        {
            float temp = Random.Range(25, 50);
            return temp;
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
        public IEnumerator Eat()
        {
            yield return new WaitForSeconds(10);
            for (int i = 0; i < 2; i++)
            {
                Hunger += Random.Range(20, 30);
                hunger = Hunger;
            }
            if (Hunger > MaxHunger)
            {
                Hunger = 100;
                hunger = Hunger;
            }

            state = CurrentState.NothingToDo;
        }

        public IEnumerator Death()
        {
            yield return new WaitForSeconds(5);
            GameManager.Ants_Arbeiter.Enqueue(gameObject);

            gameObject.SetActive(false);
        }

        private NavMeshAgent antAgent;
        private GameObject queen;

        private GameObject Semen;
        private Vector3 eatZone;
        private Vector3 queenLocation;

        public float hunger;

        private bool thirsty;
        public bool TheChosenOne { get; set; }
        public bool getFood { get; set; }

        [SerializeField]
        private CurrentState state;

        private void Awake()
        {
            Gender = RandomGender();
            if (GameManager.ArbeiterInstanzen.Count == 1)
            {
                Gender = "Male";
            }
            Name = RandomName() + " (arbeiter)";
            Health = 100;
            MaxHealth = Health;
            DeathMark = GenerateDethMark();
            Age = 0;

            Hunger = 100;
            hunger = Hunger;
            MaxHunger = Hunger;
            Energy = 100;
            MaxEnergy = Energy;
            Thirst = 100;
            MaxThirst = Thirst;
            this.gameObject.name = Name + " " + Gender;
            antAgent = gameObject.GetComponent<NavMeshAgent>();
            state = CurrentState.NothingToDo;
            queen = GameObject.Find("Queen_New_Prefab");
            queenLocation = queen.gameObject.transform.position;

            eatZone = GameObject.Find("Eatzone").transform.position;
        }

        private void Update()
        {
            if (TheChosenOne)
            {
                state = CurrentState.fertilizes_the_queen;
            }
            if (Hunger > 30 && !TheChosenOne && GameManager.StorageFood > 0)
            {
                state = CurrentState.IsEating;
            }
            if (getFood)
            {
                state = CurrentState.Bringing_Food;
            }
            if (Age >= DeathMark)
            {
                state = CurrentState.Dead;
            }

            switch (state)
            {
                case CurrentState.Bringing_Food:
                    GetFood();
                    break;

                case CurrentState.IsEating:
                    Eating();
                    break;

                case CurrentState.fertilizes_the_queen:
                    Fertilizes_the_queen();
                    break;

                case CurrentState.IsSleeping:                                                            //To Do
                    break;

                case CurrentState.NothingToDo:
                    antAgent.SetDestination(GameObject.Find("NothingLocation").transform.position);
                    break;

                case CurrentState.Dead:
                    StartCoroutine(Death());
                    break;

                default:
                    break;
            }
        }

        private void GetFood()
        {
            Semen = FoodScript.foodList[Random.Range(0, FoodScript.foodList.Count - 1)];
            bool destinationSet = false;
            Semen = GameObject.Find("Semen");
            float dist = Vector3.Distance(gameObject.transform.position, Semen.transform.position);
            float distToEatzone = Vector3.Distance(transform.position, eatZone);

            if (!destinationSet)
            {
                antAgent.SetDestination(Semen.transform.position);
                destinationSet = true;
            }

            if (dist <= 1)
            {
                Semen.transform.parent = gameObject.transform;
                Semen.transform.position = transform.position + new Vector3(0, 0.3f, 0);
                antAgent.SetDestination(eatZone);
            }
            if (distToEatzone <= 1)
            {
                Semen.transform.parent = null;
                Semen.transform.position = eatZone + new Vector3(1, 0, 1);
                Semen.name = "Food";
                if (GameManager.StorageFood <= 30 && FoodScript.foodList.Count > 0)
                {
                    GetFood();
                }
                else
                {
                    state = CurrentState.NothingToDo;
                }
            }
        }                                                                       //ToDo!

        private void Eating()
        {
            bool destinationset = false;
            float dist = Vector3.Distance(gameObject.transform.position, eatZone);

            if (!destinationset)
            {
                antAgent.SetDestination(eatZone);
            }
            if (dist <= 1)
            {
                state = CurrentState.Waiting;
                StartCoroutine(Eat());
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
                state = CurrentState.NothingToDo;
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