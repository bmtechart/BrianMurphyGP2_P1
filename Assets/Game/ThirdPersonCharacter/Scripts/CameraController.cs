using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Quaternion _targetRotation;
    private Quaternion _currentRotation;

    [SerializeField]
    private float _rotationRate;

    [SerializeField]
    private float _clampX= 75f;

    [SerializeField]
    private GameObject _followTarget;

    [SerializeField]
    private GameObject _lookAtTarget;

    [SerializeField]
    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateRotationData(Vector3 _rawRotation)
    {

        _currentRotation = _followTarget.transform.localRotation;
        Vector3 currentEuler = _currentRotation.eulerAngles;
        Vector3 targetEuler = currentEuler + _rawRotation * Time.deltaTime * _rotationRate;
        targetEuler = new Vector3(Mathf.Clamp(targetEuler.x, 0.0f, _clampX), targetEuler.y, targetEuler.z);
        _targetRotation.eulerAngles = targetEuler;
    }

    private void RotateCamera()
    {
        _followTarget.transform.localRotation = _targetRotation;
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
