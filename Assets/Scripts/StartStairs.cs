using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStairs : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			collision.GetComponent<Player>().OnStartStairsStay();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Player")
			collision.GetComponent<Player>().OnStartStairsExit();
	}
}
