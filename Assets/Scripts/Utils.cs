using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static int GetDepth(GameObject gameObj)
	{
        float y = RoundFloat(gameObj.transform.position.y, 2);
        if (y > 0)
            return 0;

        y = System.Math.Abs(y) + 0.64f;
        int depth = (int)(y / 0.64f);

        return depth;
    }
    public static Vector3 GetPosInChunk(GameObject gameObj)
	{
        float x = RoundFloat(gameObj.transform.position.x, 0) / 2.56f;
        float y = RoundFloat(gameObj.transform.position.y, 0) / 2.56f;

        return new Vector3(x, y, 0);
    }
    public static float RoundFloat(float value, int digits)
	{
        return (float)(System.Math.Round(value, digits, System.MidpointRounding.AwayFromZero));
    }
    public static Vector3 RoundVector3(Vector3 vector)
	{
        vector.x = RoundFloat(vector.x, 2);
        vector.y = RoundFloat(vector.y, 2);
        return vector;
    }
}
