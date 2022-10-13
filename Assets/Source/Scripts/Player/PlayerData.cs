using System;
using UnityEngine;

public class PlayerData
{
    public Player _player;

    public Player Player
    {
        get
        {
            if (_player is null || !_player)
                _player = GameObject.FindObjectOfType<Player>(true);
            return _player;
        }
        set => _player = value;
    }

}
