using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    #region params
    [Header("Components")]
    [SerializeField]
    CinemachineVirtualCamera _camera;

    [SerializeField]
    private Animator _animator;

    [Header("Locomotion")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _turnRate;

    [Header("Jumping")]
    [SerializeField]
    private float _jumpForce = 10f;
    [SerializeField]
    private bool _isGrounded = true;
    [SerializeField]
    private float _groundingThreshold = .75f;
    #endregion
    
    #region private:
    public Rigidbody rb;
    private Vector3 _moveDirection;

    #endregion

    #region Body
    void Start()
    {
        //if there is no hardcoded reference
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }

        //if there is no rigid body componenet at all
        if (!rb)
        {

        }

        _isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGrounded) { return; }

        Move();
        Turn();

        //update animation data
    }
    #endregion

    #region Receive Inputs
    public void UpdateMovementData(Vector3 MoveVector)
    {
        _moveDirection = MoveVector;
    }

    private Vector3 GetCameraDirection()
    {
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward * _moveDirection.z + cameraRight * _moveDirection.x;
    }

    #endregion

    #region Movement Functions

    public void Turn()
    {
        //don't bother udpdating if the character has only turned a little
        if (_moveDirection.sqrMagnitude < 0.01f) { return; }

        //get target rotation
        Quaternion rot = Quaternion.Slerp(transform.rotation,
                                            Quaternion.LookRotation(GetCameraDirection()),
                                            _turnRate);
        //update animation
        _animator.SetFloat("Turning", Mathf.Clamp(rot.eulerAngles.y - transform.rotation.eulerAngles.y, -1f, 1f));
        //move rigid body
        rb.MoveRotation(rot); 

    }

    void Move()
    {
        Vector3 movement = GetCameraDirection() * _moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position+movement); 
    }



    #endregion

    #region Jumping
    public void Jump()
    {
        //only jump if character is on ground
        if (!_isGrounded) { return; }

        Vector3 movement = GetCameraDirection();
        Vector3 jumpVector = new Vector3(movement.x, 1.0f, movement.z);
        rb.AddForce(jumpVector * _jumpForce);
        _isGrounded = false;
        _animator.SetBool("IsGrounded", _isGrounded);
        Debug.Log("started jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isGrounded) { return; }
        float collisionDot = Vector3.Dot(collision.GetContact(0).normal, Vector3.up);
        
        if (collisionDot <= _groundingThreshold) 
        { 
            _isGrounded =false;
            return; 
        }


        _isGrounded = true;
        _animator.SetBool("IsGrounded", _isGrounded);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_isGrounded) { return; }
        
        float collisionDot = Vector3.Dot(collision.GetContact(0).normal, Vector3.up);

        //if we are touching a ceiling or wall, return
        if (collisionDot >= _groundingThreshold) 
        {
            _isGrounded =false; 
            return; 
        }

        _isGrounded = true;
        _animator.SetBool("IsGrounded", _isGrounded);
    }
    #endregion
}
