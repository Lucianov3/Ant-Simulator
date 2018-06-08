using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBlockBuildNavMesh : MonoBehaviour
{
    public GameObject DirtBlock;
    private NavMeshSurface surface;
    private int temp;
    private bool isBuild;
    private NavMeshSurface[] surfaces;

	void Start ()
    {
        isBuild = true;
        temp = 10;
        surfaces = new NavMeshSurface[10];
    }

	void Update ()
    {
        if (isBuild)
        {
            for (int i = 0; i < 10; i++)
            {
                    GameObject DirtBlockClone = (GameObject)Instantiate(DirtBlock, new Vector3(0, 0, temp), Quaternion.Euler(0, 0, -90));
                    surface = DirtBlockClone.GetComponentInChildren<NavMeshSurface>();
                    surfaces[i] = DirtBlockClone.GetComponentInChildren<NavMeshSurface>();
                    surface.BuildNavMesh();
                    temp += 10;
            }
            for (int j = 0; j < surfaces.Length; j++)
            {
                surfaces[j].BuildNavMesh();
            }
            isBuild = false;
        }
    }
}
