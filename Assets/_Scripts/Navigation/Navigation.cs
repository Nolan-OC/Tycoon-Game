using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public MovementPosBase lastLocation;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void SetDestination(MovementPosBase destinationPos)
    {
        if (destinationPos == lastLocation)
        {
            Debug.Log("Same destination set, do not clearPos");
            agent.SetDestination(destinationPos.transform.position);
        }
        else
        {
            Debug.Log("Valid New Destination Set");
            //TODO need to remove npcs from list for healing. Maybe just do a cubecast up to detect npcs instead
            agent.SetDestination(destinationPos.transform.position);
            if (lastLocation != null)
                lastLocation.ClearPos(this.gameObject);
            lastLocation = destinationPos;
        }
    }

    public void navState()
    {
        //TODO if customer set to undraggable while navigating?
    }
}
