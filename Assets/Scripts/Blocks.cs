using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
	private int lastBlock = 0;

    void Start()
    {
		LoadBlocks();
	}
	private void LoadBlocks()
	{
		for (; lastBlock < transform.childCount; lastBlock++)
		{
			GameObject block = transform.GetChild(lastBlock).gameObject;
			block.SetActive(true);
			Block blockComp = block.GetComponent<Block>();
			blockComp.LoadBlock();
		}
	}
}
