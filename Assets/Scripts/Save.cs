using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Save
{
    public static void SetVec3(string key, Vector3 value)
	{
		PlayerPrefs.SetFloat($"{key}-X", value.x);
		PlayerPrefs.SetFloat($"{key}-Y", value.y);
		PlayerPrefs.SetFloat($"{key}-Z", value.z);
	}
}