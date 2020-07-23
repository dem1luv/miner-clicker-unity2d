using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			collision.GetComponent<Player>().OnStairsStay();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			collision.GetComponent<Player>().OnStairsExit();
	}
}
