 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    public Transform BuilderAnt;
    public float NodeRadius;
    public Vector2 GridSizeWorld;
    public LayerMask NoTunnelMask;
    Node[,] blockGrid;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    private void Start()
    {
        nodeDiameter = NodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(GridSizeWorld.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridSizeWorld.y / nodeDiameter);
    }

    private void Update()
    {
        CreateGrid();
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        int checkX;
        int checkY;

        checkX = node.x + 1;
        checkY = node.y;
        Check(checkX, checkY, neighbours);

        checkX = node.x - 1;
        checkY = node.y;
        Check(checkX, checkY, neighbours);

        checkX = node.x;
        checkY = node.y + 1;
        Check(checkX, checkY, neighbours);

        checkX = node.x;
        checkY = node.y - 1;
        Check(checkX, checkY, neighbours);
        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + GridSizeWorld.x / 2) / GridSizeWorld.x;
        float percentY = (worldPosition.y + GridSizeWorld.y / 2) / GridSizeWorld.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return blockGrid[x, y];
    }

    void CreateGrid()
    {
        blockGrid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridSizeWorld.x / 2 - Vector3.up * GridSizeWorld.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.up * (y * nodeDiameter + NodeRadius);
                bool buildable = Physics.CheckSphere(worldPoint, .2f, NoTunnelMask);
                blockGrid[x, y] = new Node(buildable, worldPoint, x, y);
            }
        }
    }

    void Check(int checkX, int checkY, List<Node> list)
    {
        if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
        {
            list.Add(blockGrid[checkX, checkY]);
        }
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridSizeWorld.x, GridSizeWorld.y, 1));

        if (blockGrid != null)
        {
            Node builderNode = NodeFromWorldPoint(BuilderAnt.position);
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

                if (builderNode == node)
                {
                    Gizmos.color = Color.blue;
                }

                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter -.1f));
            }
        }
    }
}
