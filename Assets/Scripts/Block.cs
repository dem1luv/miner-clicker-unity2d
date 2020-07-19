using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	private float minStrength = 4f;
	private float maxStrength = 12f;
	private float startStrength;
	private float strength;
	private SpriteRenderer spriteRenderer;
	private int blockIndex = -1;

	[SerializeField] int depth;

	public int money;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine("GenerateBlock");
	}

	IEnumerator GenerateBlock()
	{
		float y = System.Math.Abs(transform.position.y) + 0.64f;
		depth = (int)(y / 0.64f);

		while (blockIndex == -1)
		{
			for (int i = 0; i < SaveScript.blocks.Length; i++)
			{
				float chance = SaveScript.blocks[i].GetGenerationChance(depth);

				if (chance >= Random.Range(0, 1f))
				{
					blockIndex = i;
					break;
				}
			}
			if (blockIndex > -1)
			{
				break;
			}
			yield return new WaitForSeconds(0.1f);
		}

		strength = (float)System.Math.Round(Random.Range(SaveScript.blocks[blockIndex].minStrength, SaveScript.blocks[blockIndex].maxStrength), 1);
		startStrength = strength;
		money = SaveScript.blocks[blockIndex].money;
		spriteRenderer.color = SaveScript.blocks[blockIndex].color;
	}

	public float Hit(float damage)
	{
		strength -= damage;
		Color spriteColor = spriteRenderer.color;
		spriteColor.a = strength / startStrength;
		if (spriteColor.a < 0)
		{
			spriteColor.a = 0;
		}
		spriteRenderer.color = spriteColor;
		return strength;
	}
}