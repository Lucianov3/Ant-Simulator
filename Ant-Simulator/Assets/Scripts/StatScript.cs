using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatScript : MonoBehaviour
{
    private TextMeshProUGUI antName;
    private Image antGender;
    [SerializeField] private Sprite[] genderSprites;
    private TextMeshProUGUI antState;
    private StatBar healthBar;
    private StatBar energyBar;
    private StatBar hungerBar;
    private StatBar thirstBar;
    [SerializeField] private float healthBarValue;
    [SerializeField] private float healthBarMaxValue;
    [SerializeField] private float energyBarValue;
    [SerializeField] private float energyBarMaxValue;
    [SerializeField] private float hungerBarValue;
    [SerializeField] private float hungerBarMaxValue;
    [SerializeField] private float thirstBarValue;
    [SerializeField] private float thirstBarMaxValue;
    private AmeisenTypen.StandardAmeise ant;

    private void Start()
    {
        healthBar = new StatBar(healthBarValue, healthBarMaxValue, transform.GetChild(0).GetComponent<RectTransform>());
        energyBar = new StatBar(energyBarValue, energyBarMaxValue, transform.GetChild(1).GetComponent<RectTransform>());
        hungerBar = new StatBar(hungerBarValue, hungerBarMaxValue, transform.GetChild(2).GetComponent<RectTransform>());
        thirstBar = new StatBar(thirstBarValue, thirstBarMaxValue, transform.GetChild(3).GetComponent<RectTransform>());
        antName = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        antGender = transform.GetChild(5).GetComponent<Image>();
    }

    private void UpdateAllBars()
    {
        healthBar.UpdateBar(ant.ReturnStat("Health"));
        energyBar.UpdateBar(ant.ReturnStat("Energy"));
        hungerBar.UpdateBar(ant.ReturnStat("Hunger"));
        thirstBar.UpdateBar(ant.ReturnStat("Thirst"));
    }

    private void UpdateName()
    {
        antName.text = ant.name;
    }

    private void UpdateGender()
    {
        if(ant.Gender == "Male")
        {
            antGender.sprite = genderSprites[0];
        }
        else
        {
            antGender.sprite = genderSprites[1];
        }
    }

    private void UpdateState()
    {
        //antState.text = ant.State;
    }
}

public class StatBar
{
    public float Value;
    public float MaxValue;
    public RectTransform Transform;

    public StatBar(float value,float maxValue,RectTransform transform)
    {
        Value = value;
        MaxValue = maxValue;
        Transform = transform;
    }
    public void UpdateBar(float newValue)
    {
        Value = newValue;
        Transform.localScale = new Vector3(Value,Transform.localScale.y,Transform.localScale.z);
    }
}
