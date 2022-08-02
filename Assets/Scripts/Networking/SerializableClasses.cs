using UnityEngine;
using System;


[Serializable]
public class PlayerPosition
{
    public string classType = "PlayerPosition";
    public int playerId;
    public float x;
    public float y;
    public float z;
}

[Serializable]
public class AllPlayerPositions
{
    public PlayerPosition[] playerPositions;
}
