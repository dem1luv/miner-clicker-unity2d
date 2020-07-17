using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField]
    [Header("Player Characteristics")]
    [SerializeField] float horizontalSpeed = 20f;
    [SerializeField] float jumpForce = 180f;
    //private
    private Rigidbody2D rb;
    private bool isMining = false;
	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	}
    void Update()
    {
        if (!isMining)
        {
            // If player is jumping, horizontal speed 
            float xSpeed = rb.velocity.y == 0 ? horizontalSpeed : horizontalSpeed / 4f;

            // Limitation for velocity
            if (rb.velocity.x > 2f)
                rb.velocity = new Vector2(2f, rb.velocity.y);
            if (rb.velocity.x < -2f)
                rb.velocity = new Vector2(-2f, rb.velocity.y);

            // Left Move (Left Arrow)
            if (Input.GetKey(KeyCode.LeftArrow))
                rb.AddForce(Vector2.left * xSpeed);
            if (Input.GetKeyUp(KeyCode.LeftArrow))
                rb.velocity = new Vector2(0, rb.velocity.y);

            // Right Move (Right Arrow)
            if (Input.GetKey(KeyCode.RightArrow))
                rb.AddForce(Vector2.right * xSpeed);
            if (Input.GetKeyUp(KeyCode.RightArrow))
                rb.velocity = new Vector2(0, rb.velocity.y);

            // Jump (Up Arrow)
            if (Input.GetKeyDown(KeyCode.UpArrow) && rb.velocity.y == 0)
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
        // Mine (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isMining)
			{
                isMining = false;
                StopCoroutine("Mine");
            }
            else
			{
                isMining = true;
                StartCoroutine("Mine");
            }
        }
    }
    IEnumerator Mine()
	{
        while(true) {
            yield return new WaitForSeconds(2f);

            Vector2 raycastOrigin = transform.position;
            raycastOrigin.y -= 0.3f;

            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up);

            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);

                if (distance < 0.5f)
                {
                    Destroy(hit.collider.gameObject);

                    Debug.Log(hit.collider.name);
                    Debug.Log(distance);
                }
            }
        }
    }
}
