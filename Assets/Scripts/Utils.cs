using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int GetDepth(GameObject gameObj)
	{
        int depth = 0;
        if (gameObj.transform.position.y < 0)
        {
            float posY = System.Math.Abs(gameObj.transform.position.y);
            posY /= 0.64f;
            posY = (float)System.Math.Round(posY, System.MidpointRounding.AwayFromZero);
            posY *= 0.64f;
            posY += 0.64f;
            depth = (int)(posY / 0.64f);
        }
        return depth;
    }
}
