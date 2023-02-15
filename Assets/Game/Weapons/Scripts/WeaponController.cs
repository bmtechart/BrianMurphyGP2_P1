using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Vector3 forward;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void OnShoot()
    {
        //when the shoot action is first activated
    }

    public virtual void OnReleased()
    {
        //called when the shoot button is released
    }
}
