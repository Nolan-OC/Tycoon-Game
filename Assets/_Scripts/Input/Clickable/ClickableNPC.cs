using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickableNPC : ClickableBase
{
    public GameObject baseOfClicked;
    public GameObject NPCCard;

    public override void Clicked()
    {
        Debug.Log("Clicked "+ name);
    }

    private void DisplayStats()
    {
        NPCCard.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = baseOfClicked.name;
    }
}
