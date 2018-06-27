﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;

    public int gCost;
    public int hCost;
    public int x;
    public int y;
    public Node parent;


    public Node(bool walkable, Vector3 worldPosition, int x, int y)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.x = x;
        this.y = y;

    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
