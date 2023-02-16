using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerInput))]
public class CharacterController : MonoBehaviour
{

    #region components
    [Header("Movment")]
    public CharacterMovement cm;
    public float SmoothInput = 1.0f;

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Camera")]
    public GameObject playerCamera;
    public CameraController cameraController;

    [Header("Weapons")]
    public WeaponController[] EquippedWeapons;

    [Header("Animation")]
    [SerializeField]
    private Animator _animator;
    #endregion

    #region private:
    private Vector3 _rawMovement;
    private Vector3 _smoothMovement;
    #endregion

    #region Start
    void Start()
    {
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        //safety in case we forget to pass hard references
        if (!playerInput)
        {
            playerInput= GetComponent<PlayerInput>();
        }
        shootAction = playerInput.actions["Shoot"];
    }
    #endregion

    #region Update
    void Update()
    {
        SmoothMovementInput();
        cm.UpdateMovementData(_smoothMovement);
        UpdateAnimationData();
        UpdateWeaponData();
    }

    private void UpdateWeaponData()
    {
        foreach (WeaponController w in EquippedWeapons)
        {
            w.forward = cameraController.virtualCamera.transform.forward;
        }
    }

    private void UpdateAnimationData()
    {
        _animator.SetFloat("ForwardMovement", Mathf.Clamp(Mathf.Abs(_smoothMovement.z) + Mathf.Abs(_smoothMovement.x), 0, 1));
    }

    #endregion

    #region Input Action System
    private InputAction shootAction;

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 inputMoveVector = ctx.ReadValue<Vector2>();
        _rawMovement = new Vector3(inputMoveVector.x, 0.0f, inputMoveVector.y);
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {
        bool isSprinting = false;
        if(ctx.phase == InputActionPhase.Started)
        {
            isSprinting = true;
            //_animator.SetBool("IsSprinting", isSprinting);
            _animator.SetBool("IsRunning", isSprinting);
            cm.SetSprinting(isSprinting);
        }

        if(ctx.phase == InputActionPhase.Canceled)
        {
            isSprinting = false;
            _animator.SetBool("IsRunning", isSprinting);
            cm.SetSprinting(isSprinting);
        }


    }

    public void Turn(InputAction.CallbackContext ctx)
    {
        Vector2 TurnVector = ctx.ReadValue<Vector2>();

        cameraController.UpdateRotationData(new Vector3(TurnVector.y, TurnVector.x, 0.0f));
    }

    public void Dolly(InputAction.CallbackContext ctx)
    {
        float DollyAmount = ctx.ReadValue<float>();

    }



    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            cm.Jump();
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {

            foreach (WeaponController w in EquippedWeapons)
            {
                if (w == null)
                    return;

                w.OnShoot();
            }
        }


        if (ctx.phase == InputActionPhase.Canceled)
        {
            foreach (WeaponController w in EquippedWeapons)
            {
                if (w == null)
                    return;

                w.OnReleased();
            }
        }
    }
    #endregion

    #region process player input
    public void SmoothMovementInput()
    {
        _smoothMovement =  Vector3.Lerp(_smoothMovement, _rawMovement, SmoothInput);
    }
    #endregion
}
