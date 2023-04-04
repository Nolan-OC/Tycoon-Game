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
    public override void UpdatePos(GameObject newNPC)
    {
        npcCount++;
        currentNPC = newNPC;
        if (npcCount == maxNPCCount)
        {
            isFull = true;
        }
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

    public override void ClearPos(GameObject npc)
    {
        if (npcCount > 0)
        {
            isFull = false;
            currentNPC = null;
            npcCount--;
        }
        parentManager.PauseBattle();
    }

}
