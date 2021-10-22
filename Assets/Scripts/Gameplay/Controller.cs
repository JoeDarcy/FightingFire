using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    PlayerControls controls;

    MovementInputs inputs;

    private struct MovementInputs
    {
        public bool Forwards, Left, Right, Backwards;
        public Vector2 inputView;
    }

    private void Awake()
    {
        controls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyInputs();
    }

    private void ApplyInputs()
    {
        if (inputs.Forwards)
        {
            Debug.Log("works");
        }
    }

    private void GetInputs()
    {
        //inputs to move forwards
        controls.Player.MoveForward.performed += ctx => inputs.Forwards = true;
        controls.Player.MoveForward.canceled += ctx => inputs.Forwards = false;

        //inputs to move left
        controls.Player.MoveLeft.performed += ctx => inputs.Left = true;
        controls.Player.MoveLeft.canceled += ctx => inputs.Left = false;

        //inputs to move right
        controls.Player.MoveRight.performed += ctx => inputs.Right = true;
        controls.Player.MoveRight.canceled += ctx => inputs.Right = false;

        //inputs to move backwards
        controls.Player.MoveBackwards.performed += ctx => inputs.Backwards = true;
        controls.Player.MoveBackwards.canceled += ctx => inputs.Backwards = false;

        //inputs to rotate camera / "look"
        controls.Player.Look.performed += ctx => inputs.inputView = ctx.ReadValue<Vector2>();

    }

    private void OnEnable()
    {
        controls.Player.Enable();
        GetInputs(); //called here to prevent error from no garbage collector
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
