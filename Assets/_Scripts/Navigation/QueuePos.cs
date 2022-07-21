using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueuePos : MovementPosBase
{
    public void UpdatePos(GameObject newCustomer)
    {
        currentNPC = newCustomer;
        isFull = true;
    }
}
