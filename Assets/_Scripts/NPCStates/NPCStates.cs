using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStates : MonoBehaviour
{
    public enum NPCState
    {
        draggable,
        notDraggable,
        battle,
        victory,
        defeat
    }
    public NPCState state;
}
