using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public GameObject blocks;
	public GameObject block;
	public GameObject startStairs;
	public GameObject stairs;
	public string chunkId;

	private int startStairsIter = 0;
	private int stairsIter = 0;
	private Vector3 posInChunk;
	private List<GameObject> allStairs = new List<GameObject>();

	private void RemoveStairs()
	{
		foreach (GameObject obj in allStairs)
			Destroy(obj);
		allStairs.Clear();
		startStairsIter = 0;
		stairsIter = 0;
	}
	public void LoadChunk()
	{
		UpdateChunk();
		blocks.SetActive(false);
	}
	public void Move(GameObject camera)
	{
		Vector3 difference = transform.position - camera.transform.position;
		float x = difference.x;
		float y = difference.y;
		Vector3 direction;
		if (-7f > x)
			direction = Vector3.left * 2.56f * 4f;
		else if (x > 5f)
			direction = Vector3.right * 2.56f * 4f;
		else if (y > 5f)
			direction = Vector3.up * 2.56f * 6f;
		else
			direction = Vector3.down * 2.56f * 6f;
		Vector3 newPos = transform.position - direction;
		transform.position = Utils.RoundVector3(newPos);
		UpdateChunk();
	}
	public void UpdateChunk()
	{
		// calculate and set chunk id
		posInChunk = Utils.GetPosInChunk(gameObject);
		chunkId = $"({posInChunk.x};{posInChunk.y})";

		// return update function if depth > 0
		if (posInChunk.y > 0)
			return;

		// rename self
		name = $"chunk-{chunkId}";

		// remove all stairs
		RemoveStairs();

		// activate and rename blocks
		for (int i = 0; i < 16; i++)
		{
			GameObject blockObj = blocks.transform.GetChild(i).gameObject;
			blockObj.name = $"block-{chunkId}-{i}";
			blockObj.SetActive(true);
			blockObj.GetComponent<Block>().LoadBlock();
		}

		// spawn start stairs
		while (PlayerPrefs.HasKey($"startStairs-{chunkId}-{startStairsIter}-X"))
		{
			Vector3 vector3 = Load.GetVec3($"startStairs-{chunkId}-{startStairsIter}");
			GameObject startStairsInst = Instantiate(startStairs, vector3, Quaternion.identity, blocks.transform);
			allStairs.Add(startStairsInst);
			startStairsIter++;
		}

		// spawn stairs
		while (PlayerPrefs.HasKey($"stairs-{chunkId}-{stairsIter}-X"))
		{
			Vector3 vector3 = Load.GetVec3($"stairs-{chunkId}-{stairsIter}");
			GameObject stairsInst = Instantiate(stairs, vector3, Quaternion.identity, blocks.transform);
			allStairs.Add(stairsInst);
			stairsIter++;
		}
	}
	public void SaveStartStairs(GameObject startStairs)
	{
		Save.SetVec3($"startStairs-{chunkId}-{startStairsIter}", startStairs.transform.position);
		allStairs.Add(startStairs);
		startStairsIter++;
	}
	public void SaveStairs(GameObject stairs)
	{
		Save.SetVec3($"stairs-{chunkId}-{stairsIter}", stairs.transform.position);
		allStairs.Add(stairs);
		stairsIter++;
	}
}