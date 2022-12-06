using UnityEngine;

public static class ColorExtensions
{
    public static Color SetAlfa(this Color color, float alfa)
    {
        return new Color(color.r, color.g, color.b, alfa);
    }
}