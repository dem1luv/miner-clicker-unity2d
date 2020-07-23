using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int GetDepth(GameObject gameObj)
	{
        float y = gameObj.transform.position.y;
        if (y > 0)
            return 0;

        y = System.Math.Abs(y) + 0.64f;
        int depth = (int)(y / 0.64f);

        return depth;
    }
}
