using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear_PlayerController : PlayerManager
{
    public PlayerData _playerData;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _playerData= new PlayerData();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
