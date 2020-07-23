using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	private float startStrength;
	private float strength;
	private SpriteRenderer spriteRenderer;
	[SerializeField] int blockIndex = -1;
	[SerializeField] int depth;

	public int money;

	public void LoadBlock()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();

		if (PlayerPrefs.HasKey($"{name}-destroyed"))
			Destroy(gameObject);

		if (PlayerPrefs.HasKey($"{name}-blockIndex"))
			LoadData();
		else
			GenerateData();

		money = SaveScript.blocks[blockIndex].money;
		spriteRenderer.color = SaveScript.blocks[blockIndex].color;

		UpdateBlock();
	}
	public float Hit(float damage)
	{
		strength -= damage;
		PlayerPrefs.SetFloat($"{name}-strength", strength);

		UpdateBlock();

		return strength;
	}
	public void DestroyBlock()
	{
		PlayerPrefs.SetInt($"{name}-destroyed", 0);
		Destroy(gameObject);
	}
	private void LoadData()
	{
		blockIndex = PlayerPrefs.GetInt($"{name}-blockIndex");
		strength = PlayerPrefs.GetFloat($"{name}-strength");
		startStrength = PlayerPrefs.GetFloat($"{name}-startStrength");
	}
	private void GenerateData()
	{
		// blockIndex
		depth = Utils.GetDepth(gameObject);
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
		}
		PlayerPrefs.SetInt($"{name}-blockIndex", blockIndex);

		// strength
		strength = (float)System.Math.Round(Random.Range(SaveScript.blocks[blockIndex].minStrength, SaveScript.blocks[blockIndex].maxStrength), 1);
		startStrength = PlayerPrefs.GetFloat($"{name}-startStrength");
		startStrength = strength;
		PlayerPrefs.SetFloat($"{name}-strength", strength);
		PlayerPrefs.SetFloat($"{name}-startStrength", startStrength);
	}
	private void UpdateBlock()
	{
		// opacity depending on stregth
		Color spriteColor = spriteRenderer.color;
		spriteColor.a = strength / startStrength;
		if (spriteColor.a < 0)
			spriteColor.a = 0;
		spriteRenderer.color = spriteColor;
	}
}