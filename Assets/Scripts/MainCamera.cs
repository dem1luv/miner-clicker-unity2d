using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	[SerializeField] float moveTowardsSpeed = 2f;
	[SerializeField] GameObject player;
	private float extraSpeed = 1f;
	private void Update()
	{
		Vector3 target = player.transform.position;
		target.z = -10;
		float xDifference = Math.Abs(Math.Abs(transform.position.x) - Math.Abs(target.x));
		float yDifference = Math.Abs(Math.Abs(transform.position.y) - Math.Abs(target.y));
		if (yDifference > 4.2f || xDifference > 2.3f)
		{
			extraSpeed = 6f;
		}
		else if (yDifference > 3f || xDifference > 1f)
		{
			extraSpeed = 4f;
		}
		else if (xDifference <= 0.2f && yDifference <= 0.12f)
		{
			extraSpeed = 1f;
		}
		transform.position = Vector3.MoveTowards(transform.position, target, moveTowardsSpeed * extraSpeed * Time.deltaTime);
	}
}
