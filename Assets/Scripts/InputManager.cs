using System;
using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public delegate void OnMoveInput(float2 vector);
public delegate void OnAttackInput();
public delegate void OnInteractionInput();
public delegate void OnJumpInput();

public partial class InputManager : SystemBase
{
    private DefaultInputMap _defaultInputMap;

    public static OnMoveInput OnMove;
    public static OnJumpInput OnJump;
    public static OnAttackInput OnAttack;
    public static OnInteractionInput OnInteraction;

    public static float2 moveInputData;

    protected override void OnCreate()
    {
        _defaultInputMap = new DefaultInputMap();
    }

    protected override void OnDestroy()
    {
        _defaultInputMap.Dispose();
    }

    protected override void OnStartRunning()
    {
        _defaultInputMap.Enable();
    }

    protected override void OnStopRunning()
    {
        _defaultInputMap.Disable();
    }

    protected override void OnUpdate()
    {
        var curMoveInput = _defaultInputMap.Default.Movement.ReadValue<Vector2>();
        var curInteractionInput = _defaultInputMap.Default.Interact.phase;
        var curAttackInput = _defaultInputMap.Default.Attack.phase;
        var curJumpInput = _defaultInputMap.Default.Jump.phase;

        moveInputData = new float2(curMoveInput.x, curMoveInput.y);

        if (OnMove != null && curMoveInput != Vector2.zero)
        {
            OnMove.Invoke(new float2(curMoveInput.x, curMoveInput.y));
        }
        if (OnAttack != null && curAttackInput == UnityEngine.InputSystem.InputActionPhase.Started)
            OnAttack.Invoke();
        if (OnInteraction != null && curInteractionInput == UnityEngine.InputSystem.InputActionPhase.Started)
            OnInteraction.Invoke();
        if (OnJump != null && curJumpInput == UnityEngine.InputSystem.InputActionPhase.Started)
            OnJump.Invoke();
    }
}
