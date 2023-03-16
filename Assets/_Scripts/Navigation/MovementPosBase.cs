using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MovementPosBase : MonoBehaviour
{
    public int npcCount;
    public int maxNPCCount;
    public bool isFull = false;
    public GameObject currentNPC;

    [Header("NPC Movement Info")]
    public string npcAnimation;
    public Transform npcFaceDir;
    virtual public void ClearPos(GameObject npc)
    {
        if (npcCount > 0)
        {
            isFull = false;
            currentNPC = null;
            npcCount--;
        }
    }
    virtual public void UpdatePos(GameObject npc)
    {
        currentNPC = npc;
        npcCount++;
        if(npcCount == maxNPCCount)
        {
            isFull = true;
        }
    }
}
