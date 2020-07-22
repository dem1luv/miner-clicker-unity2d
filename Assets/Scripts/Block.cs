using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public float startStrength;
	public float strength;
	public SpriteRenderer spriteRenderer;
	public int blockIndex = -1;
	public int depth;
	public int money;

	void GenerateBlock()
	{
		
		

		
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