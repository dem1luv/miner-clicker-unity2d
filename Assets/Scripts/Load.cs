using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Load
{
	public static Vector3 GetVec3(string key, Vector3 defaultVal)
	{
		if (PlayerPrefs.HasKey($"{key}-X"))
		{
			Vector3 vector3;

			vector3.x = PlayerPrefs.GetFloat($"{key}-X");
			vector3.y = PlayerPrefs.GetFloat($"{key}-Y");
			vector3.z = PlayerPrefs.GetFloat($"{key}-Z");

			return vector3;
		}

		return defaultVal;
	}
	public static Vector3 GetVec3(string key)
	{
		if (PlayerPrefs.HasKey($"{key}-X"))
		{
			Vector3 vector3;

			vector3.x = PlayerPrefs.GetFloat($"{key}-X");
			vector3.y = PlayerPrefs.GetFloat($"{key}-Y");
			vector3.z = PlayerPrefs.GetFloat($"{key}-Z");

			return vector3;
		}

		return Vector2.zero;
	}
}
