using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeaponController : WeaponController
{
    [SerializeField]
    private LaserShootingBehaviour _laser;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();   
        _laser = (!_laser) ? GetComponent<LaserShootingBehaviour>() : _laser;
    }

    public override void OnShoot()
    {
        base.OnShoot();
        _laser.ShootLaser();
    }

    public override void OnReleased()
    {
        base.OnReleased();
        _laser.StopShooting();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        _laser.UpdateLaserData(transform.position, forward);
    }
}
