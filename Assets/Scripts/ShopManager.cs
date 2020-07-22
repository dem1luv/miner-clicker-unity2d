using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

	private void Start()
	{
		SaveScript.money = PlayerPrefs.GetInt("money");
	}

	private void SaveData()
	{
		if (SaveScript.money != 0)
		{
			PlayerPrefs.SetInt("money", SaveScript.money);
		}
	}

	private void OnApplicationPause(bool pause)
	{
		SaveData();
	}

	private void OnApplicationQuit()
	{
		SaveData();
	}
}
