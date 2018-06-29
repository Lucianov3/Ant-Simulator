using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool canBuild;
    public Vector3 Position;

    public int gCost;
    public int hCost;
    public int x;
    public int y;
    public Node parent;


    public Node(bool buildable, Vector3 Position, int x, int y)
    {
        this.canBuild = buildable;
        this.Position = Position;
        this.x = x;
        this.y = y;

    }

    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
