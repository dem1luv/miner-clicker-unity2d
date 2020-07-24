using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunks : MonoBehaviour
{
	private void Start()
	{
        LoadData();
	}
	private void LoadData()
	{
        // load position all child of chunks
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform chunk = transform.GetChild(i);
            chunk.position = Load.GetVec3($"chunks-child-{i}", chunk.position);
            chunk.GetComponent<Chunk>().LoadChunk();
        }
    }
    private void SaveData()
    {
        // save position for all child of chunks
        for (int i = 0; i < transform.childCount; i++)
            Save.SetVec3($"chunks-child-{i}", transform.GetChild(i).position);
    }
    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            SaveData();
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
}
