using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : PlayerController
{
    [Header("Target Objects")]
    public GameObject CameraRig;

    private PlayerInput _playerInput;

    // Start is called before the first frame update
    override protected void Start()
    {
        GameManager.Instance.sampleTest();
        //get character unit controller from unit
        //throw error if there is no unit controller on character

        //get camera controller from Camera Rig
        //throw error if there is no camera controller
    }

    // Update is called once per frame
    override protected void Update()
    {
        
    }
}
