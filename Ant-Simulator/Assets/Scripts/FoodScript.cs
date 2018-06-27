using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public static List<GameObject> foodList;
    [SerializeField] Vector3 minPos;
    [SerializeField] Vector3 maxPos;
    [SerializeField] float timeBetweenFoodDrops;  // 60 seconds IRL = 1 hour IGT || 24 minutes IRL = 1 day IGT
    float timer;
    [SerializeField] GameObject semen;
    GameObject tempSemen;
    [SerializeField]int maxFood;

    private void Start()
    {
        foodList = new List<GameObject>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenFoodDrops && foodList.Count < maxFood)
        {
            timer = 0;
            tempSemen = Instantiate(semen, new Vector3(Random.Range(minPos.x, maxPos.x), minPos.y, Random.Range(minPos.z, maxPos.z)),Quaternion.identity,transform);
            foodList.Add(tempSemen);
        }
    }
}
