using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInput : ClickableBase
{
    [SerializeField]
    private InputAction tapInput;

    private Camera mainCamera;

    //hold info
    private float holdDuration = 0.5f;
    private bool isHolding = false;
    private float pressStartTime;
    private void Awake()
    {
        mainCamera = Camera.main;
        //tapInput = playerInput.actions
        //want to auto grab actions from input action already defined
    }

    private void OnEnable()
    {
        tapInput.Enable();
        tapInput.performed += MousePressed;
        tapInput.canceled += MouseReleased;
    }
    private void OnDisable()
    {
        tapInput.performed -= MousePressed;
        tapInput.canceled -= MouseReleased;
        tapInput.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        // Set the press start time to the current time
        pressStartTime = Time.time;

        // Set isHolding to false initially
        isHolding = false;
    }

    private void MouseReleased(InputAction.CallbackContext context)
    {
        // Calculate the press duration
        float pressDuration = Time.time - pressStartTime;

        // If the press duration is shorter than the hold duration,
        // call Clicked() on the clickable object
        if (pressDuration < holdDuration && !isHolding)
        {
            //TODO make this a short press
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out ClickableBase clickable)) //base class for clickable things
                {
                    clickable.Clicked();
                }
                else
                {
                    //building card disappear
                }
            }
        }
    }
    private void Update()
    {
        // If the mouse button is still being held down and the hold duration has been reached,
        // set isHolding to true
        if (tapInput.phase == InputActionPhase.Started && Time.time - pressStartTime >= holdDuration)
        {
            isHolding = true;
        }
    }

}
