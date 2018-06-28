using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    BlockGrid blockGridScript;
    public Transform start;
    public LayerMask layer;
    SpawnBlockBuildNavMesh spawnScript;
    bool a = true;

 private void Start()
    {
        blockGridScript = GetComponent<BlockGrid>();
        spawnScript = GetComponent<SpawnBlockBuildNavMesh>();
    }

    private void Update()
    {
        FindPath(start.position, spawnScript.RoomDestination[0].transform.position);
        FindPath(start.position, spawnScript.RoomDestination[1].transform.position);
        FindPath(start.position, spawnScript.RoomDestination[4].transform.position);
    }

   
    public void FindPath(Vector3 start, Vector3 destination)
    {
        Node startNode = blockGridScript.NodeFromWorldPoint(start);
        Node destinationNode = blockGridScript.NodeFromWorldPoint(destination);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if(currentNode == destinationNode)
            {
                GetPath(startNode, destinationNode);
                print("lol");
                return;
            }

            foreach (Node neighbour in blockGridScript.GetNeighbours(currentNode))
            {
                if (!neighbour.canBuild || closedList.Contains(neighbour))
                {
                    continue;
                }

                int costToNeighbour = currentNode.gCost + 10;
                if (costToNeighbour < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = costToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, destinationNode);
                    neighbour.parent = currentNode;

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }


        }
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.x - nodeB.x);
        int distanceY = Mathf.Abs(nodeA.y - nodeB.y);

        return (distanceX * 10) + (distanceY * 10);
    }

    void GetPath(Node start, Node end)
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
            if (Physics.Raycast(path[i].worldPosition - new Vector3(0, 0, +2), transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layer))
            {
                hit.transform.gameObject.SetActive(false);
            }

        }

        blockGridScript.path = path;
    }

}
