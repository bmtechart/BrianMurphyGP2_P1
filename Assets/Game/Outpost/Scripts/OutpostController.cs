using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutpostController : MonoBehaviour
{
    [Tooltip("If the outpost is contested, it cannot be captured by any team.")]
    public bool IsContested = false;

    [SerializeField]
    [Tooltip("Time in seconds required to capture this outpost")]
    private float _captureTime = 5.0f;

    private FlagController _flagController;

    //value between 0-1 representing how captured an outpost is
    private float _captureAlpha = 0.0f;

    //capturing unit
    public GameObject CapturingUnit;

    // Start is called before the first frame update
    void Start()
    {
        //get flag controller from flag prefab
        _flagController = transform.Find("PF_Flag").gameObject.GetComponent<FlagController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        StartCoroutine(Capture());
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    IEnumerator Capture()
    {
        for (float ct = _captureTime; ct >=0.0f; ct -= Time.deltaTime)
        {
            if (IsContested) { yield return null; }
            
            _flagController.SetHeight(Mathf.InverseLerp(_captureTime, 0.0f, ct));
            yield return null;
        }
        Debug.Log("captured!");
    }
}
