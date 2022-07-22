using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionPos : MovementPosBase
{
    private ReceptionManager parentManager;
    //TODO Needs to give receptionist a position to hold, othewise errors occur when other scripts check.
    //Everyone needs to have a last location when moving or things can break

    private void Awake()
    {
        parentManager = transform.parent.GetComponent<ReceptionManager>();
    }
    public void UpdatePos(GameObject newNPC)
    {
        currentNPC = newNPC;
        isFull = true;
    }
}
