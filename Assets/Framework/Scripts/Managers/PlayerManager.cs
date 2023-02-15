using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player manager handles data 
 * that should persist independent of the pawn
 * and  be replicated to the server.
 * 
 * The player manager exists on the server and owning client only to prevent tampering.
 * 
 * For instance, what team, how many kills, points scored, current health. 
 * Another player accessing and changing these values would be bad. 
 * The game manager will need access to this information to assess the state of the game.
 */

public class PlayerManager : ManagerBase
{
 
    
    /*
     * Gets the player start with the corresponding index to this manager.
     * Creates an instance of the default pawn at that location
     * Possesses that instance of the pawn. 
     */
    public void SpawnPlayer()
    {

    }

    /*
     * Sets the unit which is currently being controlled by the player.
     * Enables that unit to receive input. 
     */

    public void PossessUnit()
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
