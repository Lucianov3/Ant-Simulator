﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    public float NodeRadius;
    public Vector2 GridSize;
    public LayerMask NoTunnelMask;
    Node[,] blockGrid;
    float nodeDiameter;
    int gridX;
    int gridY;

    private void Start()
    {
        nodeDiameter = NodeRadius * 2;
        gridX = Mathf.RoundToInt(GridSize.x / nodeDiameter);
        gridY = Mathf.RoundToInt(GridSize.y / nodeDiameter);
        CreateGrid();
    }

    private void Update()
    {
        CreateGrid();
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int x;
        int y;

        x = node.x + 1;
        y = node.y;
        Check(x, y, neighbours);

        x = node.x - 1;
        y = node.y;
        Check(x, y, neighbours);

        x = node.x;
        y = node.y + 1;
        Check(x, y, neighbours);

        x = node.x;
        y = node.y - 1;
        Check(x, y, neighbours);
        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector3 Position)
    {
        float percentX = (Position.x + GridSize.x / 2) / GridSize.x;
        float percentY = (Position.y + GridSize.y / 2) / GridSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridX - 1) * percentX);
        int y = Mathf.RoundToInt((gridY - 1) * percentY);
        return blockGrid[x, y];
    }

    void CreateGrid()
    {
        blockGrid = new Node[gridX, gridY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridSize.x / 2 - Vector3.up * GridSize.y / 2;

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.up * (y * nodeDiameter + NodeRadius);
                bool buildable = Physics.CheckSphere(worldPoint, .2f, NoTunnelMask);
                blockGrid[x, y] = new Node(buildable, worldPoint, x, y);
            }
        }
    }

    void Check(int checkX, int checkY, List<Node> list)
    {
        if (checkX >= 1 && checkX < gridX - 1 && checkY >= 1 && checkY < gridY - 1)
        {
            list.Add(blockGrid[checkX, checkY]);
        }
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridSize.x, GridSize.y, 1));

        if (blockGrid != null)
        {
            foreach (Node node in blockGrid)
            {
                if (node.canBuild)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                if (path != null)
                {
                    if (path.Contains(node))
                    {
                        Gizmos.color = Color.yellow;
                    }
                }


                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter -.1f));
            }
        }
    }
}
