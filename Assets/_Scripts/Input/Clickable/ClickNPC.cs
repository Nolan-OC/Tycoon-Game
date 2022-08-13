using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickNPC : ClickableBase
{
    public override void Clicked()
    {
        Debug.Log("Clicked NPC");
    }
}
