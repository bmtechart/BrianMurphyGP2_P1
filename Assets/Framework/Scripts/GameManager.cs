using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject DefaultPlayerController;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("There already exists an instance of the game manager. Having multiple singletons can cause unforseen issues.");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void sampleTest()
    {
        Debug.Log("Singleton called");
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
