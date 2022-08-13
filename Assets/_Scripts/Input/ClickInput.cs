using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickInput : ClickableBase
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private InputAction mouseClick;

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        //mouseClick = playerInput.actions
        //want to auto grab actions from input action already defined
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }
    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject);
            if(hit.collider.TryGetComponent(out ClickableBase clickable)) //something both customers and employees share
            {
                clickable.Clicked();
            }
        }
    }

}
