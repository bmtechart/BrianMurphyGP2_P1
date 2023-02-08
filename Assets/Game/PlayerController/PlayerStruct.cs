using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
    public int Team;
    public Color TeamColor;
    public string Name;

    //constructor, not needed but helpful
    //color must be nullable or else be a runtime constant
    public PlayerData(int team = -1, Color? color = null, string name = "player -1")
    {
        Team = team;
        TeamColor= color ?? Color.red;
        Name = name;    
    }

}
