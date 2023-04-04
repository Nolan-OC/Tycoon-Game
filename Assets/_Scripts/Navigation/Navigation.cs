using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public MovementPosBase lastLocation;
    public Animator animator;
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

            StartCoroutine(MovingToPos(destinationPos));
        }
    }

    private IEnumerator MovingToPos(MovementPosBase destination)
    {
        // play walking animation
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.1f);
        // face the objects forward pos

        if (destination.npcFaceDir != null)
        {
            transform.LookAt(destination.npcFaceDir);
            animator.Play(destination.npcAnimation);
        }
        // play the objects animation (queue pos is a standing anim, waiting room sitting, combat will be standing, and overriden when taking damage)
    }
}
