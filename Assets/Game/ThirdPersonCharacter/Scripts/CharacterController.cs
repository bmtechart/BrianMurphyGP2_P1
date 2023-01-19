using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerInput))]
public class CharacterController : Controller
{

    #region components
    public CharacterMovement cm;
    public PlayerInput playerInput;
    public GameObject playerCamera;
    public CameraController cameraController;

    [SerializeField]
    private Animator _animator;
    #endregion

    #region params
    public float SmoothInput = 1.0f;


    #endregion

    #region private:
    private Vector3 _rawMovement;
    private Vector3 _smoothMovement;
    #endregion

    void Start()
    {
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        //safety in case we forget to pass hard references
        if (!playerInput)
        {
            playerInput= GetComponent<PlayerInput>();
        }

    }

    void Update()
    {
        SmoothMovementInput();
        cm.UpdateMovementData(_smoothMovement);
        UpdateAnimationData();
    }


    #region Input Action System
    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 inputMoveVector = ctx.ReadValue<Vector2>();
        _rawMovement = new Vector3(inputMoveVector.x, 0.0f, inputMoveVector.y);
    }

    public void Turn(InputAction.CallbackContext ctx)
    {
        Vector2 TurnVector = ctx.ReadValue<Vector2>();

        cameraController.UpdateRotationData(new Vector3(TurnVector.y, TurnVector.x, 0.0f));
    }

    private void UpdateAnimationData()
    {
        _animator.SetFloat("ForwardMovement", Mathf.Clamp(Mathf.Abs(_smoothMovement.z)+Mathf.Abs(_smoothMovement.x), 0, 1));
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            cm.Jump();
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
