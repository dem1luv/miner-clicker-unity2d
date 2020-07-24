using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "blockCollider")
			collision.GetComponent<Chunk>().blocks.GetComponent<Blocks>().OnPlayerEnter();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "blockCollider")
			collision.GetComponent<Chunk>().blocks.SetActive(false);
	}
}