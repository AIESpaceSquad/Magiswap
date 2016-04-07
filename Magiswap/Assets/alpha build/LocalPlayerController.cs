using UnityEngine;
using System.Collections;

public class LocalPlayerController : MonoBehaviour {

    [SerializeField]
    string controllerName = "";

    [SerializeField]
    float moveSpeed = 10;
    [SerializeField]
    float jumpForce = 15;

    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    GameObject frontCheck;
    [SerializeField]
    LayerMask layermask;

    float groundRadius = 0.2f;

    bool isFacingRight = true;
    bool isGrounded = true;

    Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if  (Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, layermask) || 
             Physics2D.OverlapCircle(groundCheck.transform.position, groundRadius, 1 << gameObject.layer)) {//bitshift my layer an int into a layemask
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && Input.GetButtonDown(controllerName + "_jump"))
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        float move = Input.GetAxis(controllerName + "_moveX");

        if (isGrounded)
        {
            myRigidbody.velocity = new Vector2(move * moveSpeed, myRigidbody.velocity.y);
        }
        else
        {
            myRigidbody.AddForce(new Vector2(move * moveSpeed, 0));
            myRigidbody.velocity = new Vector2(Mathf.Clamp(myRigidbody.velocity.x, -moveSpeed, moveSpeed), myRigidbody.velocity.y);
        }

        if (isGrounded)
        {
            if ((move > 0 && !isFacingRight) || 
                (move < 0 && isFacingRight))
            {
                Flip();
            }
        }
	}

    void Update()
    {

    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
