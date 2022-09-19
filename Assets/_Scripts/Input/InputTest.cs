using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputTest : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction click;
    public InputAction hold;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        click.Enable();
        click.performed += Click;

        hold.Enable();
        hold.performed += Hold;
    }
    private void OnDisable()
    {
        click.Enable();
        click.performed += Click;

        hold.Enable();
        hold.performed += Hold;
    }

    private void Click(InputAction.CallbackContext context)
    {
        Debug.Log("Clicked");
    }
    private void Hold(InputAction.CallbackContext context)
    {
        Debug.Log("Holding click");
    }
}
