using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
	public void OnPlayerEnter()
	{
		// hide blocks if depth > 0
		if (transform.position.y > 0.1f)
			gameObject.SetActive(false);
		else
			gameObject.SetActive(true);
	}
}
