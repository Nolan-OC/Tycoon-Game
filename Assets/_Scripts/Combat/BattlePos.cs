using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePos : MovementPosBase
{
    public bool isCustomerPos;
    private BattleManager parentManager;

    private void Awake()
    {
        parentManager = transform.parent.GetComponent<BattleManager>();
    }
    public void UpdatePos(GameObject newNPC)
    {
        currentNPC = newNPC;
        isFull = true;
        //update battlemanager customer/employee battle will not start unless they are set
        if(currentNPC.TryGetComponent(out Employee employee))
        {
            parentManager.employee = employee;
        }
        else if(currentNPC.TryGetComponent(out Customer customer))
        {
            parentManager.customer = customer;
        }
        parentManager.StartBattle();
    }
}
