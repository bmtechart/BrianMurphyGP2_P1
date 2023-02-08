using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/*
 * 
 * The game manager sets up and stores the basic conditions of the game, such as how many teams there are, how many players, etc. 
 * The game manager only exists on the server.
 * 
 * Key Tasks:
 *  1. Spawn game state object
 *  2. Spawn Player Controllers
 *  2. Get player start location(s) and spawn player pawns
 *
*/

public class GameManager : MonoBehaviour
{
    public enum PlayerType { Human, AI };

    public static GameManager Instance { get; private set; }

    public PlayerManager[] playerControllers;

    public PlayerManager playerController;
    public AIController aiController;

    public PlayerType[] Players;

    #region Awake
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

    #endregion

    #region Start
    void Start()
    {
        //for each player, create a player controller
        CreatePlayerManagers();
    }
    #endregion

    #region Functions

    void CreatePlayerManagers()
    {
        foreach (PlayerType p in Players)
        {
            switch (p)
            {
                case PlayerType.Human:
                    if (playerController)
                    {
                        Instantiate(playerController, transform);
                    }
                    break;
                //create player controller object
                case PlayerType.AI:
                    Debug.Log("created AI player!");

                    break;
                    //create AI controller
            }
        }
    }

    #endregion
    void Update()
    {
        
    }
}
