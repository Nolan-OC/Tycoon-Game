using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBuilding : ClickableBase
{
    public GameObject baseOfClicked;    //returns the main script holder, clickable is found on GFX child
    public GameObject buildingCard;     //building card reveals upgrades lvl and info about building to player
    public override void Clicked()
    {
        Debug.Log("Clicked " + baseOfClicked.name);
    }
}
