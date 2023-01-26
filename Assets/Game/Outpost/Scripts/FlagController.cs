using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlagController : MonoBehaviour
{
    [Header("Flagpole Settings")]
    [SerializeField]
    private float _flagpoleScaleY = 10.0f;

    [Header("Flag Settings")]
    [SerializeField]
    private float _flagScaleY = 1.0f;
    [SerializeField]
    private float _flagScaleX = 1.0f;
    [SerializeField]
    private Color _flagColor;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _flagHeight=0.0f;

    private Transform _flagPoleRoot;
    private Transform _flagRoot;
    private GameObject _flag;
    // Start is called before the first frame update
    void Start()
    {
        _flagRoot = transform.Find("FlagRoot");
        _flag = transform.Find("FlagRoot/Flag").gameObject;
    }

    public void SetHeight(float height)
    {
        _flagHeight = height;

        _flagRoot.position = new Vector3(_flagRoot.position.x,
            Mathf.Lerp(0.0f, (_flagpoleScaleY - _flagScaleY) + transform.position.y, _flagHeight),
            _flagRoot.position.z);
    }

    private void GameplayUpdate()
    {

    }

    private void EditorUpdate()
    {
        //set flagpole transforms
        _flagPoleRoot = transform.Find("FlagpoleRoot");
        Vector3 newScale = new Vector3(1, _flagpoleScaleY, 1);
        _flagPoleRoot.localScale = newScale;

        //set flag transforms

        _flagRoot = transform.Find("FlagRoot");
        _flagRoot.localScale = new Vector3(_flagScaleX, _flagScaleY, 1.0f);


        //set flag color
        _flag = transform.Find("FlagRoot/Flag").gameObject;
        _flag.GetComponent<MeshRenderer>().material.color = _flagColor;

        //move flag up and down flag pole

        _flagRoot.position = new Vector3(_flagRoot.position.x,
            Mathf.Lerp(0.0f, (_flagpoleScaleY - _flagScaleY)+transform.position.y, _flagHeight),
            _flagRoot.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            EditorUpdate();
        }
    }
}
