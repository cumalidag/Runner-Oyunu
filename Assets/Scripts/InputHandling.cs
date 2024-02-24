using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandling : MonoBehaviour
{
    // Ýnput giriþlerini kontrol eder.
    private ControlScheme controlScheme;

    public event EventHandler OnJump;
    public event EventHandler OnSlip;
    public event EventHandler OnMoveRight;
    public event EventHandler OnMoveLeft;

    public static InputHandling Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        controlScheme = new ControlScheme();
    }

    private void OnEnable()
    {
        controlScheme.Enable();
    }

    private void OnDisable()
    {
        controlScheme.Disable();
    }

    private void Start()
    {
        controlScheme.Player.Jump.performed += OnJumpPerformed;
        controlScheme.Player.Slip.performed += OnSlipPerformed;
        controlScheme.Player.MoveRight.performed += OnMoveRightPerformed;
        controlScheme.Player.MoveLeft.performed += OnMoveLeftPerformed;

    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    private void OnSlipPerformed(InputAction.CallbackContext context)
    {
        OnSlip?.Invoke(this, EventArgs.Empty);
    }

    private void OnMoveRightPerformed(InputAction.CallbackContext context)
    {
        OnMoveRight?.Invoke(this, EventArgs.Empty);
    }

    private void OnMoveLeftPerformed(InputAction.CallbackContext context)
    {
        OnMoveLeft?.Invoke(this, EventArgs.Empty);
    }
    
}
