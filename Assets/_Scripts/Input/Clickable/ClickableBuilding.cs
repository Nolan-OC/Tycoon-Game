using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableBuilding : ClickableBase
{
    public GameObject baseOfClicked;    //returns the main script holder, clickable is found on GFX child
    public GameObject buildingCard;     //building card reveals upgrades lvl and info about building to player

    private TextMeshPro cardName;
    private TextMeshPro cardLevel;
    private GameObject upgradeButton;

    private void Start()
    {
        if (buildingCard == null)
            Debug.LogError("NO BUILDING CARD FOUND FOR " + gameObject.name);

        //TODO not sure if this necessary or the way it is done in DisplayStats();
        cardName = buildingCard.transform.Find("Name (TMP)").GetComponent<TextMeshPro>();
        cardLevel = buildingCard.transform.Find("Level (TMP)").GetComponent<TextMeshPro>();
        upgradeButton = buildingCard.transform.Find("Upgrade Button").gameObject;
    }
    public override void Clicked()
    {
        Debug.Log("Clicked " + baseOfClicked.name);
        DisplayStats();
    }

    private void DisplayStats()
    {
        //buildingCard children MUST be in order of name,level,upgradeCost
        buildingCard.SetActive(true);
        buildingCard.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text=baseOfClicked.name;
        buildingCard.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text= baseOfClicked.GetComponent<BuildingStats>().level.ToString();
        buildingCard.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text= "Cost: "+baseOfClicked.GetComponent<BuildingStats>().upgCost.ToString();
    }
}
