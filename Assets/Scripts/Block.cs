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
		if (PlayerPrefs.HasKey($"{name}-destroyed"))
			Destroy(gameObject);

		if (PlayerPrefs.HasKey($"{name}-blockIndex"))
			blockIndex = PlayerPrefs.GetInt($"{name}-blockIndex");
		else
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

					yield return new WaitForEndOfFrame();
				}
				if (blockIndex > -1)
				{
					break;
				}
			}

			PlayerPrefs.SetInt($"{name}-blockIndex", blockIndex);
		}

		if (PlayerPrefs.HasKey($"{name}-strength"))
			strength = PlayerPrefs.GetFloat($"{name}-strength");
		else
		{
			strength = (float)System.Math.Round(Random.Range(SaveScript.blocks[blockIndex].minStrength, SaveScript.blocks[blockIndex].maxStrength), 1);
			PlayerPrefs.SetFloat($"{name}-strength", strength);
		}

		if (PlayerPrefs.HasKey($"{name}-startStrength"))
			startStrength = PlayerPrefs.GetFloat($"{name}-startStrength");
		else
		{
			startStrength = strength;
			PlayerPrefs.SetFloat($"{name}-startStrength", startStrength);
		}

		money = SaveScript.blocks[blockIndex].money;
		spriteRenderer.color = SaveScript.blocks[blockIndex].color;

		Color spriteColor = spriteRenderer.color;
		spriteColor.a = strength / startStrength;
		if (spriteColor.a < 0)
		{
			spriteColor.a = 0;
		}
		spriteRenderer.color = spriteColor;
	}

	public float Hit(float damage)
	{
		strength -= damage;
		PlayerPrefs.SetFloat($"{name}-strength", strength);
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