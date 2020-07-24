using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "blockCollider")
            collision.GetComponent<Chunk>().Move(gameObject);
    }
}