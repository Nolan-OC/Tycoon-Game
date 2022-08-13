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
    //TODO navigate customers to positions in the break room not just to the center
    public void Start()
    {
        StartCoroutine(HealingNPCs());
    }
    public void UpdatePos(GameObject newNPC)
    {
        employees.Add(newNPC);
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
