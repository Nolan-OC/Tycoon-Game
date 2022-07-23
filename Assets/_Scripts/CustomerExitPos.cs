using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerExitPos : MovementPosBase
{
    public List<GameObject> customers;
    //isFull comes from movePosBase, and is never set to full so that this pos can hold multiple people
    //TODO delete customers as they arrive
    public void UpdatePos(GameObject newNPC)
    {
        customers.Add(newNPC);
    }
}
