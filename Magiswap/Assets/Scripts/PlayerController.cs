using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidBody;
    Vector2 jump;
    bool isGrounded;
    public int jumpForce;
    public int movementForce;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    KeyCode spacebar = KeyCode.Space;


    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jump = new Vector3(0, jumpForce);
        isGrounded = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //isGrounded = true;
	}

    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag == "Ground")
            isGrounded = true;
    }

    public float PlayerInput()
    {
        OnUpLadder();
        OnJump();
        return OnMoveLeftRight();
    }

    float OnMoveLeftRight()
    {
        if (Input.GetKey(left))
        {
            return -movementForce * Time.deltaTime;
        }
        else if (Input.GetKey(right))
        {
            return movementForce * Time.deltaTime;
        }
        return 0;
    }

    void OnJump()
    {
        if (Input.GetKeyDown(spacebar) && isGrounded)
        {
            rigidBody.AddForce(jump);
            isGrounded = false;
        }
    }

    void OnUpLadder()
    {
        //for ladders?
        if (Input.GetKey(up))
        {

        }
    }
}



