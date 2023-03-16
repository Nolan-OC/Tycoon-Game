using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInput : ClickableBase
{
    [SerializeField]
    private InputAction tapInput;

    private Camera mainCamera;
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
    }
    private void OnDisable()
    {
        tapInput.performed -= MousePressed;
        tapInput.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        //TODO make this a short press
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.TryGetComponent(out ClickableBase clickable)) //base class for clickable things
            {
                clickable.Clicked();
            }
            else
            {
                //building card dissappear
            }
        }
    }

}
