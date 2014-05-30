using UnityEngine;
using System.Collections;

public static class MovementUtility {

    public static Vector2 ProportionalForce(Vector2 targetVelocity, Vector2 actualVelocity, Vector2 minForce, Vector2 maxForce, float proportionalConstant, bool xEnabled = true, bool yEnabled = true)
    {
        Vector2 returned = new Vector2();

        Vector2 error = targetVelocity - actualVelocity;
        returned = error * proportionalConstant;
        returned = ClampVector(returned, minForce, maxForce);

        if (!xEnabled)
        {
            returned.x = 0;
        }
        if (!yEnabled)
        {
            returned.y = 0;
        }

        return returned;
    }

    public static Vector2 ClampVector(Vector2 input, Vector2 min, Vector2 max)
    {
        Vector2 returned = input;

        if (min != null)
        {
            if (returned.x < min.x)
                returned.x = min.x;
            if (returned.y < min.y)
                returned.y = min.y;
        }

        if (max != null)
        {
            if (returned.x > max.x)
                returned.x = max.x;
            if (returned.y > max.y)
                returned.y = max.y;
        }

        return returned;
    }
	
}
