using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnBlockBuildNavMesh : MonoBehaviour
{
    public Vector3[] RoomPosition;
    public GameObject[] RoomDestination;
    public GameObject DirtBlock;
    private float tempX;
    private float tempY;
    private bool isBuild;
    GameObject[,] blocks;

	void Start ()
    {
        blocks = new GameObject[30, 30];
        RoomDestination = new GameObject[5];
           RoomPosition = new Vector3[5];
        isBuild = true;
        tempX = -29;
        tempY = 29f;
    }

	void Update ()
    {
        if (isBuild)
        {
            for (int i = 0; i < 30; i++)
            {
                tempX = -29;
                for (int j = 0; j < 30; j++)
                {
                    GameObject DirtBlockClone = (GameObject)Instantiate(DirtBlock, new Vector3(tempX, tempY, 0), Quaternion.Euler(0, 0, 0));
                    tempX += 2;
                    blocks[i, j] = DirtBlockClone;
                    if(i == 0  || i == 29 || j == 0 || j == 29)
                    {
                        DirtBlockClone.layer = 11;
                    }
                }
                    
                    tempY -= 2;
            }
            blocks[0, 14].SetActive(false);
            blocks[0, 15].SetActive(false);
            PrepareRooms();
            isBuild = false;
        }
    }

    void PrepareRooms()
    {
        int x = Random.Range(3, 10);
        int y = Random.Range(3, 15);
        RoomDestination[0] = blocks[x, y];
        RoomPosition[0] = RoomDestination[0].transform.position + new Vector3(0, -4, 0);
        x += 2;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y + 1; j >= y - 1; j--)
            {
                blocks[i, j].name = "Queen";
                blocks[i, j].layer = 11;
            }
        }

        x = Random.Range(12, 18);
        y = Random.Range(3, 15);
        RoomDestination[1] = blocks[x, y];
        RoomPosition[1] = RoomDestination[1].transform.position + new Vector3(0, -4, 0);
        x += 2;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y + 1; j >= y - 1; j--)
            {
                blocks[i, j].name = "1";
                blocks[i, j].layer = 11;
            }
        }

        x = Random.Range(20, 27);
        y = Random.Range(3, 15);
        RoomDestination[2] = blocks[x, y];
        RoomPosition[2] = RoomDestination[2].transform.position + new Vector3(0, -4, 0);
        x += 2;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y + 1; j >= y - 1; j--)
            {
                blocks[i, j].name = "2";
                blocks[i, j].layer = 11;
            }
        }

        x = Random.Range(3, 10);
        y = Random.Range(17, 27);
        RoomDestination[3] = blocks[x, y];
        RoomPosition[3] = RoomDestination[3].transform.position + new Vector3(0, -4, 0);
        x += 2;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y + 1; j >= y - 1; j--)
            {
                blocks[i, j].name = "3";
                blocks[i, j].layer = 11;
            }
        }

        x = Random.Range(12, 22);
        y = Random.Range(17, 27);
        RoomDestination[4] = blocks[x, y];
        RoomPosition[4] = RoomDestination[4].transform.position + new Vector3(0, -4, 0);
        x += 2;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y + 1; j >= y - 1; j--)
            {
                blocks[i, j].name = "Food";
                blocks[i, j].layer = 11;
            }
        }


        RoomDestination[0].name = "QueenRoom";
        RoomDestination[1].name = "Room1";
        RoomDestination[2].name = "Room2";
        RoomDestination[3].name = "Room3";
        RoomDestination[4].name = "FoodRoom";


    }
}
