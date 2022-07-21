using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPosBase : MonoBehaviour
{
    public bool isFull = false;
    public GameObject currentNPC;
    public void ClearPos()
    {
        isFull = false;
        currentNPC = null;
    }
}
