using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionManager : MonoBehaviour
{
    [Header("Receptionist")]
    [SerializeField]private WaitingRoomPos rPos;

    [Header("Queue Info")]
    public List<QueuePos> queuePositions;
    public List<QueuePos> waitingRoomPositions;

    [Header ("Customer Creation")]
    public GameObject pCustomer;
    public Transform customerSpawnLocation;
    //TODO waiting room layout currently set manually, however I want to have upgrades to the waiting room which
    //would include more chairs, different layouts. Need to build a system that will allow for different layouts
    //and set them accordingly. Probably read the parent of all waiting positions and save different but disabled
    //parents with new layouts/more chairs. Then just activate/deactivate on upgrades.
    private void Start()
    {
        StartCoroutine(MoveQueueTimer());
        NewCustomer();
        NewCustomer();
        NewCustomer();
        NewCustomer();
        NewCustomer();
    }
    private void Update()
    {
        
    }
    private IEnumerator MoveQueueTimer()
    {
        while (true)
        {
            //TODO timer currently independent of receptionist, should maybe restart timer when he moves from rPos
            yield return new WaitForSecondsRealtime(5);

            //if there is a current receptionist...
            if (rPos.isFull)
            {
                //check if customer can move on based on reception skill
                //check how much of customer stats are revealed to player based on reception skill
                MoveQueue();
            }
        }
    }
    public void MoveQueue()
    {
        if (queuePositions[0].isFull && HasWaitingSpace())  //Move customer0 to waiting room
        {
            //TODO Receptionist checks customer stats
            queuePositions[0].currentNPC.GetComponent<NPCStates>().state = NPCStates.NPCState.draggable;
            NavToWaitingRoom(queuePositions[0].currentNPC);
        }

        for (int i = 0; i < queuePositions.Count; i++) //MoveQueueDown
        {
            if (queuePositions[i].isFull)
            {
                QueuePos newPosition = LowestEmptyPos();    //Find lowest empty position
                QueuePos oldPosition = queuePositions[i];

                if (newPosition == null)
                {
                    Debug.Log("Queue is full but move was called");
                    return;
                }

                newPosition.UpdatePos(oldPosition.currentNPC);  //update new position with customer
                oldPosition.currentNPC.GetComponent<Navigation>().SetDestination(newPosition);   //nav to new position

                oldPosition.ClearPos(oldPosition.currentNPC);   //clear previous position
            }
        }
    }
    private QueuePos LowestEmptyPos()
    {
        foreach(QueuePos position in queuePositions)
        {
            if (!position.isFull)

                return position;
        }
        return null;
    }
    private void receptionCheckStats()
    {
        //todo logic for checking stats based on reception skill
    }
    public void NewCustomer()
    {   
        QueuePos emptySpace = LowestEmptyPos();
        if (emptySpace != null)
        {
            Debug.Log("Creating new customer");
            GameObject newCustomer = Instantiate(pCustomer, customerSpawnLocation.position, Quaternion.identity);

            //random stats for customer
            newCustomer.GetComponent<Customer>().aggro = Random.Range(1, 11);
            newCustomer.GetComponent<Customer>().reason = Random.Range(1, 11);
            newCustomer.GetComponent<Customer>().kindness = Random.Range(1, 11);

            //nav to queue position
            newCustomer.GetComponent<Navigation>().SetDestination(emptySpace);
            emptySpace.isFull = true;
            emptySpace.currentNPC = newCustomer;
        }
        else
            Debug.Log("Queue full");
    }
    private bool HasWaitingSpace()
    {
        foreach (QueuePos position in waitingRoomPositions)
        {
            if (position.isFull == false)
                return true;
        }
        return false;
    }
    private void NavToWaitingRoom(GameObject customer)
    {
        //moves customer to random empty position in waiting room
        int rndWaitingPos = Random.Range(0, waitingRoomPositions.Count);

        //Confirm waiting pos is not taken
        bool validPosChosen = false;
        while (validPosChosen == false)
        {
            if (waitingRoomPositions[rndWaitingPos].isFull)
                rndWaitingPos = Random.Range(0, waitingRoomPositions.Count);
            else
                validPosChosen = true;
        }
        QueuePos newPos = waitingRoomPositions[rndWaitingPos];

        newPos.isFull = true;
        newPos.currentNPC = customer;
        customer.GetComponent<Navigation>().SetDestination(newPos);
    }
}
