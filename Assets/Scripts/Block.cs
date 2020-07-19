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

	private void Start()
	{
		strength = (float)System.Math.Round(Random.Range(minStrength, maxStrength), 1);
		startStrength = strength;
		spriteRenderer = GetComponent<SpriteRenderer>();
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
