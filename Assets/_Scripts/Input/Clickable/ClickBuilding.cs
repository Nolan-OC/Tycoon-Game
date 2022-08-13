using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBuilding : ClickableBase
{
    public override void Clicked()
    {
        Debug.Log("Clicked Building");
        //TODO Clicking building clicks the gfx rather than base object. Need to find parent object
    }
}
