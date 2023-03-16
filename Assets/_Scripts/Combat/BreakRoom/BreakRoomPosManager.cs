using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoomPosManager : MovementPosBase
{
    public List<GameObject> employees;
    public List<MovementPosBase> positions;
    //public int roomCount; roomcount is the employees count
    //isFull comes from movePosBase, and is only set to full when a max count has been reached, depending on break room upgrade level
    //TODO navigate customers to positions in the break room not just to the center
    //TODO move coroutine, roomMax count and a list of Pos' to a break room manager class, Pos should only hold gameobject
    public void Start()
    {
        //Set position by child object gameobject positions = transform.
        StartCoroutine(HealingNPCs());
    }
    public override void UpdatePos(GameObject npc)
    {
        currentNPC = npc;
        employees.Add(npc);
        npcCount++;

        //TODO need to rework break room system as the npc doesn't leave properly due to the extra layers of positions
        // breakroom has multiple postions, so now we will organize npcs into positions
        foreach(MovementPosBase position in positions)
        {
            if (!position.isFull)
            {
                position.UpdatePos(currentNPC);
                break;
            }
        }

        if (npcCount == maxNPCCount)
        {
            isFull = true;
        }
    }
    private IEnumerator HealingNPCs()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            foreach (GameObject npc in employees)
            {
                if (npc.TryGetComponent(out Employee npcStats))
                {
                    //Healing
                    npc.GetComponent<Employee>().TakeDamage(-5);

                    //Stop overhealing
                    if (npcStats.patience >= npcStats.maxPatience)
                        npcStats.patience = npcStats.maxPatience;
                }
            }
        }
    }
    override public void ClearPos(GameObject npc)
    {
        //had to make inherited class take a gamobejct argument, maybe not the most elegant
        employees.Remove(npc);
        isFull = false;
        currentNPC = null;
    }
}
