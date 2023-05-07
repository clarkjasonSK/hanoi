using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeverHelper
{
    public static float ConvertEulerAngle(float angle)
    {
        // if angle is greater than left side of lever, subtact full circle angle to get right side
        return angle > 90 ? angle - 360 : angle;
    }
}
