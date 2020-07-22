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
		int chunkId = transform.parent.GetComponent<BlockCollider>().chunkId;
		for (; lastBlock < transform.childCount; lastBlock++)
		{
			GameObject block = transform.GetChild(lastBlock).gameObject;
			block.SetActive(true);
			Block blockComp = block.GetComponent<Block>();
			blockComp.spriteRenderer = block.GetComponent<SpriteRenderer>();
			if (PlayerPrefs.HasKey($"{block.name}-destroyed"))
				Destroy(block);
			if (PlayerPrefs.HasKey($"{blockComp.name}-blockIndex"))
				blockComp.blockIndex = PlayerPrefs.GetInt($"{blockComp.name}-blockIndex");
			else
			{
				float y = System.Math.Abs(transform.position.y) + 0.64f;
				blockComp.depth = (int)(y / 0.64f);

				while (blockComp.blockIndex == -1)
				{
					for (int e = 0; e < SaveScript.blocks.Length; e++)
					{
						float chance = SaveScript.blocks[e].GetGenerationChance(blockComp.depth);

						if (chance >= Random.Range(0, 1f))
						{
							blockComp.blockIndex = e;
							break;
						}
					}
					if (blockComp.blockIndex > -1)
					{
						break;
					}
				}

				PlayerPrefs.SetInt($"{blockComp.name}-blockIndex", blockComp.blockIndex);
			}

			if (PlayerPrefs.HasKey($"{blockComp.name}-strength"))
				blockComp.strength = PlayerPrefs.GetFloat($"{blockComp.name}-strength");
			else
			{
				blockComp.strength = (float)System.Math.Round(Random.Range(SaveScript.blocks[blockComp.blockIndex].minStrength, SaveScript.blocks[blockComp.blockIndex].maxStrength), 1);
				PlayerPrefs.SetFloat($"{blockComp.name}-strength", blockComp.strength);
			}

			if (PlayerPrefs.HasKey($"{name}-startStrength"))
				blockComp.startStrength = PlayerPrefs.GetFloat($"{blockComp.name}-startStrength");
			else
			{
				blockComp.startStrength = blockComp.strength;
				PlayerPrefs.SetFloat($"{blockComp.name}-startStrength", blockComp.startStrength);
			}

			blockComp.money = SaveScript.blocks[blockComp.blockIndex].money;
			blockComp.spriteRenderer.color = SaveScript.blocks[blockComp.blockIndex].color;

			Color spriteColor = blockComp.spriteRenderer.color;
			spriteColor.a = blockComp.strength / blockComp.startStrength;
			if (spriteColor.a < 0)
			{
				spriteColor.a = 0;
			}
			blockComp.spriteRenderer.color = spriteColor;
		}
	}
}
