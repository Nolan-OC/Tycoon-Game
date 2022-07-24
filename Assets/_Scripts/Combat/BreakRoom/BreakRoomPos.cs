using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoomPos : MovementPosBase
{
    public List<GameObject> employees;
    public int roomMaxCount;
    //public int roomCount; roomcount is the employees count
    //isFull comes from movePosBase, and is never set to full so that this pos can hold multiple people
    //TODO delete customers as they arrive
    public void UpdatePos(GameObject newNPC)
    {
        employees.Add(newNPC);
    }
}
