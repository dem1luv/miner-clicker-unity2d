using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //[SerializeField] & public
    [Header("Player Characteristics")]
    [SerializeField] float horizontalSpeed = 20f;
    [SerializeField] float jumpForce = 180f;

    public static float miningDelay = 2.4f;
    public static float minPower = 1f;
    public static float maxPower = 3f;

    [Header("UI")]
    [SerializeField] Text moneyText;
    [SerializeField] Text depthText;

    [Header("Others")]
    [SerializeField] GameObject stairsStart;
    [SerializeField] GameObject stairs;

    //private
    private Rigidbody2D rb;
    private bool isMining = false;
    private bool isOnStairs = false;
    private bool isOnStartStairs = false;
    private bool isClimbing = false;
	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	}
    void Update()
    {
        int depth = 0;
        if (transform.position.y < 0) {
            float playerY = System.Math.Abs(transform.position.y);
            playerY /= 0.64f;
            playerY = (float)System.Math.Round(playerY, System.MidpointRounding.AwayFromZero);
            playerY *= 0.64f;
            playerY += 0.64f;
            depth = (int)(playerY / 0.64f);
        }
        depthText.text = $"Depth: {depth}";
        if (!isMining && isOnStairs && isClimbing) {
            rb.MovePosition(rb.position + Vector2.up * 4f * Time.deltaTime);
            rb.velocity = Vector2.zero;
        }
        else if (!isMining)
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
        // Climbe (C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            isClimbing = !isClimbing;
            if (!isOnStairs && isClimbing)
			{
                isClimbing = false;
			}
            if (isMining == true)
			{
                isMining = false;
                StopCoroutine("Mine");
			}
        }
    }
    IEnumerator Mine()
	{
        while (rb.velocity != Vector2.zero) {
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(miningDelay);
        if (!isOnStairs && !isOnStartStairs)
        {
            Vector2 raycastOrigin = transform.position;

            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up);

            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);

                if (distance < 0.5f)
                {
                    Vector3 stairsPos = hit.collider.gameObject.transform.position;
                    stairsPos.y += 0.64f;

                    transform.position = stairsPos;

                    stairsPos.z = 1;

                    Instantiate(stairsStart, stairsPos, Quaternion.identity);
                }
            }
        }
        while (true) {
            yield return new WaitForSeconds(miningDelay);

            Vector2 raycastOrigin = transform.position;

            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up);

            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);

                if (distance < 0.5f)
                {
                    Block blockComponent = hit.collider.GetComponent<Block>();
                    float damage = (float)System.Math.Round(Random.Range(minPower, maxPower), 1);
                    float strength = blockComponent.Hit(damage);
                    if (strength <= 0)
                    {
                        Vector3 stairsPos = hit.collider.gameObject.transform.position;

                        transform.position = stairsPos;

                        stairsPos.z = 1;

                        GameManager.ChangeMoney(blockComponent.money);

                        Destroy(hit.collider.gameObject);
                        Instantiate(stairs, stairsPos, Quaternion.identity);
                    }
                }
            }
        }
    }
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.tag == "stairs")
		{
            isOnStairs = true;
		}
        else if (collision.tag == "stairsStart")
        {
            isOnStartStairs = true;
            if (!isMining && isOnStairs && isClimbing)
            {
                rb.MovePosition(rb.position + new Vector2(-0.64f, 0.64f));
                isClimbing = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "stairs" && !isMining)
		{
            isOnStairs = false;
        }
        else if (collision.tag == "stairsStart")
		{
            isOnStartStairs = false;
        }

    }
}
