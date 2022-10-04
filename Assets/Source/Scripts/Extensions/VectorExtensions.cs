using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 Round(this Vector2 vector, float step)
    {
        return new Vector2(vector.x.Round(step), vector.y.Round(step));
    }

    public static Vector3 Round(this Vector3 vector, float step)
    {
        return new Vector3(vector.x.Round(step), vector.y.Round(step), vector.z.Round(step));
    }

    public static Vector2Int Round(this Vector2Int vector, int step)
    {
        return new Vector2Int(vector.x.Round(step), vector.y.Round(step));
    }

    public static Vector3Int Round(this Vector3Int vector, int step)
    {
        return new Vector3Int(vector.x.Round(step), vector.y.Round(step), vector.z.Round(step));
    }

    public static Vector3Int ToInt(this Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }

    public static Vector2Int ToInt(this Vector2 vector)
    {
        return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }
}