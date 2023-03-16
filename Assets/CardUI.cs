using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardUI : MonoBehaviour
{
    public ClickableBuilding building;
    public void OpenCard(GameObject buildObj)
    {
        building = buildObj.GetComponent<ClickableBuilding>();

        transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = buildObj.gameObject.name;
        transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = buildObj.GetComponent<BuildingStats>().level.ToString();
        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "Cost: " + buildObj.GetComponent<BuildingStats>().upgCost.ToString();
    }
    public void CloseCard()
    {
        building = null;
        gameObject.SetActive(false);
    }
}
