using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUIScript : MonoBehaviour
{
    public enum UITypes
    {
        AntCount,FoodCount
    }
    TextMeshProUGUI text;
    [SerializeField] UITypes type;


    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        switch (type)
        {
            case UITypes.AntCount:
                text.text = GameManager.CurrentAnts.ToString();
                break;
            case UITypes.FoodCount:
                text.text = GameManager.StorageFood.ToString();
                break;
        }
    }
}