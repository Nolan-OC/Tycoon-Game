using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomPos : MovementPosBase
{
    private ReceptionManager parentManager;
    //Everyone needs to have a last location when moving or things can break

    private void Awake()
    {
        parentManager = transform.parent.GetComponent<ReceptionManager>();
    }

}
