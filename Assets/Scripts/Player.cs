using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField]
    [Header("Player Speed")]
    [SerializeField] float horizontalSpeed = 20f;
    [SerializeField] float jumpForce = 400f;
    //private
    private Rigidbody2D rb;
	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	}
	void Update()
    {
        // If player is jumping
        float xSpeed = rb.velocity.y == 0 ? horizontalSpeed : horizontalSpeed / 10;

        // Left Move (Left Arrow)
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.AddForce(new Vector2(-xSpeed, 0));
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            rb.velocity = new Vector2(0, rb.velocity.y);
        
        // Right Move (Right Arrow)
        if (Input.GetKey(KeyCode.RightArrow))
            rb.AddForce(new Vector2(xSpeed, 0));
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            rb.velocity = new Vector2(0, rb.velocity.y);

        // Jump (Up Arrow)
        if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
