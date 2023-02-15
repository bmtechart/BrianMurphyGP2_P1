using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShootingBehaviour : MonoBehaviour
{

    #region Variables
    private LineRenderer _lineRenderer;
    private Vector3 Origin;
    private Vector3 Direction;
    #endregion

    #region Params
    [Header("Rendering")]
    //range of trace
    [Tooltip("Range of line trace. If set to -1, the range is infinite.")]
    public float Range = -1.0f;

    private float MaxRendereringRange = 10000f;


    //range of line renderer
    [Tooltip("The maximum distance the laser can render.")]
    public float RenderRange;

    [Tooltip("By default the line renderer should have a width of 1 unity unit. This value scales the width of the renderer.")]
    [SerializeField]
    private float _lrWidthMultiplier = 2f;

    [Header("Damage")]
    [Tooltip("Rate of damage per second dealt as laser lingers on a target.")]
    public float DamagePerSecond = 10.0f;

    private bool isShooting = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        #region Initialize Line Renderer Component
        _lineRenderer = GetComponent<LineRenderer>();
        
        //add a line renderer component if none exists
        if(!_lineRenderer)
        {
            gameObject.AddComponent<LineRenderer>();
            _lineRenderer = GetComponent<LineRenderer>();
        }

        //line renderer disabled by default
        _lineRenderer.enabled = false;

        _lineRenderer.widthMultiplier = _lrWidthMultiplier;
        #endregion


        //set trace range
        Range = (Range == -1.0f) ? Mathf.Infinity : Range;
    }

    void Update()
    {
        if (!isShooting)
        {
            return;
        }

        RaycastHit _hit;
        if (!Physics.Raycast(Origin, Direction, out _hit, Range))
        {
            RenderLaser(transform.position, Origin + (Direction * MaxRendereringRange));

        }

        if (Physics.Raycast(Origin, Direction, out _hit, Range))
        {
            //if hit object has damageable interface
            //damage object dps * time.deltatime
            RenderLaser(transform.position, _hit.point);
        }
    }

    public void UpdateLaserData(Vector3 rayOrigin, Vector3 rayDirection)
    {
        Origin = rayOrigin;
        Direction = rayDirection;
    }

    public void ShootLaser()
    {

        isShooting = true;
        _lineRenderer.enabled = true;
    }

    public void StopShooting()
    {

        isShooting = false;
        _lineRenderer.enabled = false;
    }
    

    private void RenderLaser(Vector3 pointA, Vector3 pointB)
    {
        _lineRenderer.SetPosition(0, pointA);
        _lineRenderer.SetPosition(1, pointB);
    }
}
