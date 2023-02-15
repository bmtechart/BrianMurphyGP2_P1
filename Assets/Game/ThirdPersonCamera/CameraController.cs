using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;

    private Quaternion _targetRotation;
    private Quaternion _currentRotation;

    [SerializeField]
    private float _rotationRate;

    [SerializeField]
    private float _maxClampX= 75f;
    [SerializeField]
    private float _minClampX = -20.0f;

    [SerializeField]
    private GameObject _followTarget;

    [SerializeField]
    private GameObject _lookAtTarget;

    [SerializeField]
    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        if (!virtualCamera)
        {
            Debug.Log("No Virtual Camera Present!");
        }
    }

    public void UpdateRotationData(Vector3 _rawRotation)
    {

        _currentRotation = _followTarget.transform.localRotation;
        Vector3 currentEuler = _currentRotation.eulerAngles;
        Vector3 targetEuler = currentEuler + _rawRotation;
        //targetEuler.x = Mathf.Clamp(targetEuler.x, _minClampX, _maxClampX);
        _targetRotation.eulerAngles = targetEuler;
    }

    private void RotateCamera()
    {
        _followTarget.transform.localRotation = Quaternion.Slerp(_followTarget.transform.localRotation, _targetRotation, Time.deltaTime * _rotationRate);
    }

    private void Dolly()
    {

    }

    private void FollowTarget()
    {
        if (_followTarget != null && _lookAtTarget != null)
        {
            _followTarget.transform.position = _lookAtTarget.transform.position;
            _followTarget.transform.position += _cameraOffset;
        }
    }

    private void Update()
    {
        FollowTarget();
        RotateCamera();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        //RotateCamera();
    }
}
