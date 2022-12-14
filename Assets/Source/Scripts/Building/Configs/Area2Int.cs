using System;
using UnityEngine;

[Serializable]
public struct Area2Int
{
    public Vector2Int Min;
    public Vector2Int Max;

    public bool PointInArea(Vector2Int point)
    {
        return point.x >= Min.x && point.y >= Min.y 
            && point.x <= Max.x && point.y <= Max.y;
    }

    public bool PointInArea(Vector3Int point)
    {
        return PointInArea(new Vector2Int(point.x, point.z));
    }
}