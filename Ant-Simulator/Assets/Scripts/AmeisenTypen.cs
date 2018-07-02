using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.AI;
using System;

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
        public float DistNothing { get; protected set; }

        public Animator Animator;
        public NavMeshAgent Agent;

        public Vector3 tempDestination;

        public string RandomName()
        {
            if (Gender == "Male")
                Name = GameManager.NameListM[UnityEngine.Random.Range(0, GameManager.NameListM.Count)];
            else
                Name = GameManager.NameListW[UnityEngine.Random.Range(0, GameManager.NameListW.Count)];

            return Name;
        }

        public enum CurrentState
        { Bringing_Food, IsEating, fertilizes_the_queen, IsSleeping, NothingToDo, Waiting, Dead, IsDrinking, Build }

        public string RandomGender()
        {
            int temp = UnityEngine.Random.Range(1, 3);
            if (temp == 1)
                Gender = "Male";
            else
                Gender = "Female";
            return Gender;
        }

        public float GenerateDethMark()
        {
            float temp = UnityEngine.Random.Range(25, 50);
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
                Hunger += UnityEngine.Random.Range(20, 30);
                hunger = Hunger;
                GameManager.StorageFood -= 1;
            }
            if (Hunger > MaxHunger)
            {
                Hunger = 100;
                hunger = Hunger;
            }

            State = CurrentState.NothingToDo;
        }

        public IEnumerator Drink()
        {
            yield return new WaitForSeconds(5);
            Thirst += 70;
            if (Thirst > 100)
            {
                Thirst = 100;
            }
            antAgent.isStopped = false;
            State = CurrentState.NothingToDo;
        }

        public IEnumerator Death()
        {
            yield return new WaitForSeconds(5);
            GameManager.Ants_Arbeiter.Enqueue(gameObject);

            gameObject.SetActive(false);
        }

        public IEnumerator Sleep()
        {
            yield return new WaitForSeconds(10);
            Energy = 100;
            antAgent.isStopped = false;
            State = CurrentState.NothingToDo;
        }

        public IEnumerator HungerC()
        {
            yield return new WaitForSeconds(60);
            if (Hunger != 0)
            {
                Hunger -= 5;
            }
            else
            {
                Health -= 10;
            }
            if (Thirst != 0)
            {
                Thirst -= 20;
            }
            else
            {
                Health -= 10;
            }
            StartCoroutine(HungerC());
        }

        public IEnumerator ToAge()
        {
            yield return new WaitForSeconds(1440);
            Age++;
            StartCoroutine(ToAge());
        }

        private NavMeshAgent antAgent;
        private GameObject queen;

        private GameObject lake;
        private GameObject Astar;

        private Pathfinding pathfinding;

        private GameObject Semen;
        private Vector3 eatZone;
        private Vector3 queenLocation;
        private Vector3 BedRoom1;
        private Vector3 BedRoom2;
        private Vector3 BedRoom3;

        public float hunger;
        private bool thirsty;
        public bool TheChosenOne { get; set; }
        public bool getFood { get; set; }

        public CurrentState State { get; set; }

        [SerializeField]
        public string State1;

        [SerializeField]
        public float energy;

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
            State = CurrentState.Waiting;
            queen = GameObject.Find("Queen_New_Prefab");
            queen.AddComponent<AmeisenTypen.Königen>();
            queenLocation = queen.gameObject.transform.position;
            Astar = GameObject.Find("A*");
            eatZone = Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[4];                                                     //Position ändern
            BedRoom1 = Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[1];
            BedRoom2 = Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[2];
            BedRoom3 = Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[3];
            lake = GameObject.Find("DrinkZone");
            pathfinding = Astar.GetComponent<Pathfinding>();
            Animator = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            StartCoroutine(ToAge());
            StartCoroutine(HungerC());
        }

        private void Update()
        {
            hunger = Hunger;
            energy = Energy;
            State1 = State.ToString();
            if (GameManager.CurrentAnts >= 30 && !TheChosenOne)
            {
            }
            if (Thirst < 0)
            {
                Thirst = 0;
            }
            if (Hunger < 0)
            {
                Hunger = 0;
            }
            if (Energy < 0)
            {
                Energy = 0;
            }
            if (Age >= DeathMark || Health <= 0)
            {
                State = CurrentState.Dead;
            }
            if (TheChosenOne && Energy != 0 && State != CurrentState.Dead)
            {
                State = CurrentState.fertilizes_the_queen;
            }
            if (Hunger <= 30 && !TheChosenOne && GameManager.StorageFood > 0 && Energy != 0 && State != CurrentState.Dead)
            {
                State = CurrentState.IsEating;
            }
            if (Thirst <= 30 && !TheChosenOne && Energy != 0 && State != CurrentState.Dead)
            {
                State = CurrentState.IsDrinking;
            }
            if (getFood && Energy != 0 && State != CurrentState.Dead)
            {
                State = CurrentState.Bringing_Food;
            }
            if (Energy == 0 && State != CurrentState.fertilizes_the_queen && State != CurrentState.IsSleeping && State != CurrentState.Dead)
            {
                State = CurrentState.IsSleeping;
            }

            switch (State)
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

                case CurrentState.IsSleeping:
                    GoTOBedRoom();//To Do
                    break;

                case CurrentState.NothingToDo:
                    Vector3 temp = GameManager.NothingToDoV3[UnityEngine.Random.Range(0, GameManager.NothingToDoV3.Count - 1)];
                    DistNothing = Vector3.Distance(transform.position, temp);
                    antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = temp;
                    antAgent.SetDestination(temp);

                    State = CurrentState.Waiting;
                    break;

                case CurrentState.Dead:
                    StartCoroutine(Death());
                    break;

                case CurrentState.IsDrinking:
                    Drinking();
                    break;

                case CurrentState.Waiting:
                    if (DistNothing < 1)
                    {
                        State = CurrentState.NothingToDo;
                    }
                    break;

                default:
                    break;
            }
            Animator.SetBool("isMoving", !antAgent.isStopped);
        }

        private void GoTOBedRoom()
        {
            int randomN = 0;
            randomN = UnityEngine.Random.Range(1, 4);

            switch (randomN)
            {
                case 1:
                    if (GameManager.BedCounter < 33)
                    {
                        float dist = Vector2.Distance(transform.position, BedRoom1);
                        antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = BedRoom1;
                        antAgent.SetDestination(BedRoom1);
                        if (dist <= 1.2)
                        {
                            antAgent.isStopped = true;
                            StartCoroutine(Sleep());
                        }
                    }
                    else
                    {
                        GoTOBedRoom();
                    }
                    break;

                case 2:
                    if (GameManager.BedCounter2 < 33)
                    {
                        float dist = Vector2.Distance(transform.position, BedRoom2);
                        antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = BedRoom2;
                        antAgent.SetDestination(BedRoom2);
                        if (dist <= 1.2)
                        {
                            antAgent.isStopped = true;
                            StartCoroutine(Sleep());
                        }
                    }
                    else
                    {
                        GoTOBedRoom();
                    }
                    break;

                case 3:
                    if (GameManager.BedCounter3 < 34)
                    {
                        float dist = Vector2.Distance(transform.position, BedRoom3);
                        antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = BedRoom3;
                        antAgent.SetDestination(BedRoom3);
                        if (dist <= 1.2)
                        {
                            antAgent.isStopped = true;
                            StartCoroutine(Sleep());
                        }
                    }
                    else
                    {
                        GoTOBedRoom();
                    }
                    break;

                default:
                    break;
            }
        }

        private void GetFood()
        {
            Semen = FoodScript.foodList[UnityEngine.Random.Range(0, FoodScript.foodList.Count - 1)];
            bool destinationSet = false;
            Semen = GameObject.Find("Semen");
            float dist = Vector3.Distance(gameObject.transform.position, Semen.transform.position);
            float distToEatzone = Vector3.Distance(transform.position, eatZone);

            if (!destinationSet)
            {
                antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = Semen.transform.position;
                antAgent.SetDestination(Semen.transform.position);
                destinationSet = true;
            }

            if (dist <= 1)
            {
                Semen.transform.parent = gameObject.transform;
                Semen.transform.position = transform.position + new Vector3(0, 0.3f, 0);
                antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = eatZone;
                antAgent.SetDestination(eatZone);
            }
            if (distToEatzone <= 1)
            {
                Semen.transform.parent = null;
                Semen.transform.position = eatZone + new Vector3(1, 0, 1);
                Semen.name = "Food";
                GameManager.StorageFood += 1;
                if (GameManager.StorageFood <= 30 && FoodScript.foodList.Count > 0 && State != CurrentState.fertilizes_the_queen)
                {
                    GetFood();
                }
                else
                {
                    Energy -= 20;
                    State = CurrentState.NothingToDo;
                }
            }
        }

        private void Drinking()
        {
            bool destinationset = false;
            float dist = Vector3.Distance(transform.position, lake.transform.position);
            antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = lake.transform.position;
            antAgent.SetDestination(lake.transform.position);

            if (!destinationset)
            {
                antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = eatZone;
                antAgent.SetDestination(eatZone);
            }
            if (dist <= 1.5)
            {
                State = CurrentState.Waiting;
                Energy -= 10;
                StartCoroutine(Drink());
            }
        }

        private void Eating()
        {
            bool destinationset = false;
            float dist = Vector3.Distance(gameObject.transform.position, eatZone);

            if (!destinationset)
            {
                antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = eatZone;
                antAgent.SetDestination(eatZone);
            }
            if (dist <= 1)
            {
                State = CurrentState.Waiting;
                Energy -= 10;

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
                antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = queenLocation;
                antAgent.SetDestination(queenLocation);
            }
            if (dist <= 2)
            {
                queen.GetComponent<KI_Rigina_formica>().SpawnLarva();
                antAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = transform.position;
                antAgent.SetDestination(transform.position);
                Energy -= 20;
                State = CurrentState.NothingToDo;
            }
        }
    }

    public class Soldat : StandardAmeise
    {
        private NavMeshAgent SoldatAgent;

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

            Animator = GetComponent<Animator>();

            gameObject.AddComponent<PatrolScript>();
        }

        public float hunger;
        private bool thirsty;
    }

    public class Königen : StandardAmeise
    {
        private NavMeshAgent queenAgent;
        private GameObject Astar;
        private Pathfinding pathfinding;
        private float Dist;


        bool bla;

        private void Start()
        {
            Astar = GameObject.Find("A*");
            pathfinding = Astar.GetComponent<Pathfinding>();
            queenAgent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            
        }

        private void Update()
        {
            if (Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition != null && !bla)
            {
                queenAgent.SetDestination(Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[0]);
                queenAgent.GetComponent<AmeisenTypen.StandardAmeise>().tempDestination = Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[0];
                GetComponent<KI_Rigina_formica>().Larvae.transform.position = Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[0];
                bla = true;
            }
            Dist = Vector3.Distance(transform.position, Astar.GetComponent<SpawnBlockBuildNavMesh>().RoomPosition[0]);
            if (Dist <= 0.5)
            {
                queenAgent.isStopped = true;
            }
        }
    }
}