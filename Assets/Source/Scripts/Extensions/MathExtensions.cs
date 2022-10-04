﻿
public static class MathExtensions
{
    public static float Round(this float value, float step)
    {
        bool isNegative = value < 0;

        if (isNegative)
            value = -value;

        float remains = value % step;

        float result = remains >= step / 2f ? value + (step - remains) : value - remains;

        return isNegative ? -result : result;
    }

    public static int Round(this int value, int step)
    {
        bool isNegative = value < 0;

        if (isNegative)
            value = -value; 

        int remains = value % step;

        int result = remains >= step / 2f ? value + (step - remains) : value - remains;

        return isNegative ? -result : result;
    }
}
