using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class MovementPosBase : MonoBehaviour
{
    public bool isFull = false;
    public GameObject currentNPC;
    virtual public void ClearPos(GameObject npc)
    {
        isFull = false;
        currentNPC = null;
    }
}
