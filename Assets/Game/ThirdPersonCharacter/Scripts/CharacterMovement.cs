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
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        if (_moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rot = Quaternion.Slerp(transform.rotation,
                                                Quaternion.LookRotation(GetCameraDirection()),
                                                _turnRate);

            //i
            _animator.SetFloat("Turning", Mathf.Clamp(rot.eulerAngles.y - transform.rotation.eulerAngles.y, -1f, 1f));
            //Debug.Log(rot.eulerAngles.y - transform.rotation.eulerAngles.y);
            rb.MoveRotation(rot);

            
        }
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
        rb.AddForce(transform.up * _jumpForce);
        _isGrounded = false;
        _animator.SetBool("IsGrounded", _isGrounded);
        Debug.Log("started jump");
    }
    //trace for ground
    private void OnCollisionEnter(Collision collision)
    {

        if (Vector3.Dot(collision.GetContact(0).normal, Vector3.up) >= _groundingThreshold)
        {
            if (_isGrounded)
            {
                return;
            }


           _isGrounded = true;
           _animator.SetBool("IsGrounded", _isGrounded);
        }

        //Debug.Log(Vector3.Dot(collision.GetContact(0).normal, Vector3.up));

        //check dot product of collision normal with up vector
        //set grounded if dot product is equal to 1
    }
    #endregion
}
