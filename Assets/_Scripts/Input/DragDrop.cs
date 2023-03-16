using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class DragDrop : MonoBehaviour
{
    [SerializeField]
    private InputAction holdInput;
    [SerializeField]
    private float mouseDragSpeed = .1f;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        //tapInput = playerInput.actions
        //want to auto grab actions from input action already defined
    }

    private void OnEnable()
    {
        holdInput.Enable();
        holdInput.performed += HoldInputDetected;
    }
    private void OnDisable()
    {
        holdInput.performed -= HoldInputDetected;
        holdInput.Disable();
    }

    private void HoldInputDetected(InputAction.CallbackContext context)
    {
        //TODO grab clicked object at ClickInput so raycast isn't cast .5 seconds after click when drag detected
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.TryGetComponent(out NPCStates states)) //something both customers and employees share
            {
                if (states.state != NPCStates.NPCState.draggable)
                    return;

                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }
    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        //TODO create visual component to show valid drop locations

        //disable navmesh to allow object to float
        clickedObject.GetComponent<NavMeshAgent>().enabled = false;

        while (holdInput.ReadValue<float>() != 0)
        {
            Vector3 collisionPos = RayCollisionPos();
            Debug.DrawRay(mainCamera.transform.position, collisionPos - mainCamera.transform.position, Color.red);

            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, new Vector3(collisionPos.x,collisionPos.y+3,collisionPos.z), ref velocity, mouseDragSpeed);
            yield return null;
        }

        DropNPC(clickedObject);
    }
    private void DropNPC(GameObject clickedObject)
    {
        //reenable navmeshagent to turn gravity back on
        clickedObject.GetComponent<NavMeshAgent>().enabled = true;

        //Check if valid drop was made
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        bool isCustomer = false;
        if (clickedObject.TryGetComponent(out Customer customer))
            isCustomer = true;
        else if (clickedObject.TryGetComponent(out Employee employee))
            isCustomer = false;
        else
            Debug.LogError("ERROR RAY CAST HIT INVALID TARGET, NOT CUSTOMER OR EMPLOYEE");

        //TODO swap employees between stations for quality of life
        //TODO Consider breaking up into two functions, one for customer and one for employees?
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent(out Dropable drop))
            {
                drop.DropNPC(clickedObject, isCustomer);
            }
        }
    }
    private void InvalidDropLocation(GameObject clickedObject)
    {
        //Dropped on invalid location, teleport clickedobject back to where they started
        clickedObject.transform.position = clickedObject.GetComponent<Navigation>().lastLocation.transform.position;
        clickedObject.GetComponent<Navigation>().SetDestination(clickedObject.GetComponent<Navigation>().lastLocation);
    }
    private Vector3 RayCollisionPos()
    {
        Vector3 returnVal = new Vector3(0,0,0);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        // create a plane at 0,0,0 whose normal points to +Y:
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        // Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
        float distance = 0;
        // if the ray hits the plane...
        if (hPlane.Raycast(ray, out distance))
        {
            // get the hit point:
            returnVal = ray.GetPoint(distance);
        }
        return returnVal;
    }

}
