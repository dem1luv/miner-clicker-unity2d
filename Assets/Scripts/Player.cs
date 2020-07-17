using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField]
    [Header("Player Speed")]
    [SerializeField] float horizontalSpeed = 3f;
    [SerializeField] float jumpForce = 180f;
    //private
    private Rigidbody2D rb;
	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	}
	void Update()
    {
        // If player is jumping, horizontal speed 
        float xSpeed = rb.velocity.y == 0 ? horizontalSpeed : horizontalSpeed / 1.3f;

        // Left Move (Left Arrow)
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(new Vector2(-xSpeed, 0) * Time.deltaTime);
        
        // Right Move (Right Arrow)
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(new Vector2(xSpeed, 0) * Time.deltaTime);

        // Jump (Up Arrow)
        if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
