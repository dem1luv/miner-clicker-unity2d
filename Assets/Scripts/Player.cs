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
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.AddForce(new Vector2(-horizontalSpeed, 0));
        if (Input.GetKey(KeyCode.RightArrow))
            rb.AddForce(new Vector2(horizontalSpeed, 0));
        if (Input.GetKeyDown(KeyCode.UpArrow))
            rb.AddForce(new Vector2(0, jumpForce));
    }
}
