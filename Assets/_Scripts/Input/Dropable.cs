using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropable : MonoBehaviour
{
    public MovementPosBase employeePos, customerPos;

    public void DropNPC(GameObject npc, bool isCustomer)
    {
        if(isCustomer)
        {
            if (customerPos == null)
                InvalidDropLocation(npc);

            if (customerPos.isFull)
            {
                Debug.Log(customerPos.name + " is already full");
                InvalidDropLocation(npc);
                return;
            }
            npc.GetComponent<Navigation>().SetDestination(customerPos);
            customerPos.UpdatePos(npc);
        }
        else
        {
            if (employeePos == null)
                InvalidDropLocation(npc);

            if (employeePos.isFull)
            {
                Debug.Log(employeePos.name + " is already full");
                InvalidDropLocation(npc);
                return;
            }
            npc.GetComponent<Navigation>().SetDestination(employeePos);
            employeePos.UpdatePos(npc);
        }
    }
    private void InvalidDropLocation(GameObject clickedObject)
    {
        //Dropped on invalid location, teleport clickedobject back to where they started
        clickedObject.transform.position = clickedObject.GetComponent<Navigation>().lastLocation.transform.position;
        clickedObject.GetComponent<Navigation>().SetDestination(clickedObject.GetComponent<Navigation>().lastLocation);
    }
}
