using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject DirtWall;
    [SerializeField]
    GameObject CurveBotRightBlock;
    [SerializeField]
    GameObject CurveBotLeftBlock;
    [SerializeField]
    GameObject CurveTopRightBlock;
    [SerializeField]
    GameObject CurveTopLeftBlock;
    [SerializeField]
    GameObject HorizontalBlock;
    [SerializeField]
    GameObject TBlock;
    [SerializeField]
    GameObject VerticalBlock;
    [SerializeField]
    GameObject[] BlockList;

    GameObject[,] WallList;
    int[,] possibleSpaces;
    int tempy;
    int tempz;
    int currentLine;
    int currentCollumn;
    int tempLine = 0;
    int tempCollumn = 4;

	void Start ()
    {
        tempy = 0;
        tempz = 0;
        possibleSpaces = new int[3, 2];
        WallList = new GameObject[10, 10];

        for (int i = 0; i < 10; i++)
        {
            tempz = 0;
            for (int j = 0; j < 10; j++)
            {
                GameObject WallBlockClone = (GameObject)Instantiate(DirtWall, new Vector3(0, tempy, tempz), Quaternion.Euler(0, 0, 180));
                WallList[i, j] = WallBlockClone;
                tempz += 5;
            }
            tempy -= 5;
        }
        Destroy(WallList[0, 4]);

        GameObject TBlockClone = (GameObject)Instantiate(TBlock, new Vector3(0, 0, 20), Quaternion.Euler(180, 0, -90));
        WallList[0, 4] = TBlockClone;
    }
	
	void Update ()
    {
        Possibilities(WallList[0,4]);
    }

    public void Possibilities(GameObject currentObject)
    {
        currentLine = tempLine;
        currentCollumn = tempCollumn;
        if (currentObject.name == "CurveBotRight(Clone)")
        {
            possibleSpaces[0, 1] = currentCollumn + 1;
            possibleSpaces[0, 0] = currentLine;
            possibleSpaces[1, 0] = currentLine + 1;
            possibleSpaces[1, 1] = currentCollumn;
        }
        else if (currentObject.name == "CurveBotLeft(Clone)")
        {
            possibleSpaces[0, 1] = currentCollumn - 1;
            possibleSpaces[0, 0] = currentLine;
            possibleSpaces[1, 0] = currentLine + 1;
            possibleSpaces[1, 1] = currentCollumn;
        }
        else if (currentObject.name == "CurveTopLeft(Clone)")
        {
            possibleSpaces[0, 1] = currentCollumn - 1;
            possibleSpaces[0, 0] = currentLine;
            possibleSpaces[1, 0] = currentLine - 1;
            possibleSpaces[1, 1] = currentCollumn;
        }
        else if (currentObject.name == "CurveTopRight(Clone)")
        {
            possibleSpaces[0, 1] = currentCollumn + 1;
            possibleSpaces[0, 0] = currentLine;
            possibleSpaces[1, 0] = currentLine - 1;
            possibleSpaces[1, 1] = currentCollumn;
        }
        else if (currentObject.name == "T_Shape(Clone)")
        {
            possibleSpaces[0, 1] = currentCollumn - 1;
            possibleSpaces[0, 0] = currentLine;
            possibleSpaces[1, 1] = currentCollumn + 1;
            possibleSpaces[1, 0] = currentLine;
            possibleSpaces[2, 0] = currentLine + 1;
            possibleSpaces[2, 1] = currentCollumn;
        }
        else if (currentObject.name == "Horizontal(Clone)")
        {
            possibleSpaces[0, 0] = currentLine - 1;
            possibleSpaces[0, 1] = currentCollumn;
            possibleSpaces[1, 0] = currentLine + 1;
            possibleSpaces[1, 1] = currentCollumn;
        }
        else if (currentObject.name == "Vertical(Clone)")
        {
            possibleSpaces[0, 1] = currentCollumn - 1;
            possibleSpaces[0, 0] = currentLine;
            possibleSpaces[1, 1] = currentCollumn + 1;
            possibleSpaces[1, 0] = currentLine;
        }
        int tempPossible = -1;
        for (int k = 0; k < 3; k++)
        {
            if (WallList[possibleSpaces[k, 0], possibleSpaces[k, 1]].name == "Ground1(Clone)")
            {
                tempPossible++;
            }
        }
        int r = Random.Range(0, tempPossible + 1);
        currentLine = possibleSpaces[r, 0];
        currentCollumn = possibleSpaces[r, 1];
        print(currentLine);
        print(currentCollumn);
        print(tempPossible);
    }
}
