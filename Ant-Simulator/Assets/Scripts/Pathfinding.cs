using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    public Transform start;
    public LayerMask layer;
    public int Index;
    private BlockGrid blockGridScript;
    private SpawnBlockBuildNavMesh spawnScript;
    private GameObject wall;
    private NavMeshSurface surface;
    public bool HasBuild;
    private bool queenRoom;
    private bool room1;
    private bool room2;
    private bool room3;
    private bool foodRoom;

    private void Start()
    {
        blockGridScript = GetComponent<BlockGrid>();
        spawnScript = GetComponent<SpawnBlockBuildNavMesh>();
        wall = GameObject.Find("Wall");
        surface = wall.GetComponent<NavMeshSurface>();
    }

    private void Update()
    {
        if (spawnScript.RoomDestination[0] != null && !HasBuild)
        {
            Index = 0;
            FindPath(start.position, spawnScript.RoomDestination[0].transform.position);
            Index = 1;
            FindPath(start.position, spawnScript.RoomDestination[1].transform.position);
            Index = 2;
            FindPath(start.position, spawnScript.RoomDestination[2].transform.position);
            Index = 3;
            FindPath(start.position, spawnScript.RoomDestination[3].transform.position);
            Index = 4;
            FindPath(start.position, spawnScript.RoomDestination[4].transform.position);
        }
    }

    public void FindPath(Vector3 start, Vector3 destination)
    {
        Node Start = blockGridScript.NodeFromWorldPoint(start);
        Node Destination = blockGridScript.NodeFromWorldPoint(destination);

        List<Node> open = new List<Node>();
        HashSet<Node> closed = new HashSet<Node>();
        open.Add(Start);

        while (open.Count > 0)
        {
            Node current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].FCost < current.FCost || open[i].FCost == current.FCost && open[i].hCost < current.hCost)
                {
                    current = open[i];
                }
            }

            open.Remove(current);
            closed.Add(current);

            if (current == Destination)
            {
                GetPath(Start, Destination);
                return;
            }

            foreach (Node neighbour in blockGridScript.GetNeighbours(current))
            {
                if (!neighbour.canBuild || closed.Contains(neighbour))
                {
                    continue;
                }

                int costToNeighbour = current.gCost + 10;
                if (costToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = costToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, Destination);
                    neighbour.parent = current;

                    if (!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                    }
                }
            }
        }
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.x - nodeB.x);
        int distanceY = Mathf.Abs(nodeA.y - nodeB.y);

        return (distanceX * 10) + (distanceY * 10);
    }

    private void GetPath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        for (int i = 0; i < path.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(path[i].Position - new Vector3(0, 0, +2), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer))
            {
                hit.transform.gameObject.SetActive(false);
            }
        }

        CarveRoomByIndex();
    }

    private void CarveRoomByIndex()
    {
        if (Index == 0)
        {
            while (GameObject.Find("Queen") != null)
            {
                GameObject.Find("Queen").SetActive(false);
                queenRoom = true;
            }
        }
        else if (Index == 1)
        {
            while (GameObject.Find("1") != null)
            {
                GameObject.Find("1").SetActive(false);
                room1 = true;
            }
        }
        else if (Index == 2)
        {
            while (GameObject.Find("2") != null)
            {
                GameObject.Find("2").SetActive(false);
                room2 = true;
            }
        }
        else if (Index == 3)
        {
            while (GameObject.Find("3") != null)
            {
                GameObject.Find("3").SetActive(false);
                room3 = true;
            }
        }
        else
        {
            while (GameObject.Find("Food") != null)
            {
                GameObject.Find("Food").SetActive(false);
                foodRoom = true;
            }
        }

        surface.BuildNavMesh();
        if (room3 && room2 && room1 && foodRoom && queenRoom)
        {
            HasBuild = true;
            blockGridScript.GridNeedsUpdate = false;
        }
    }
}