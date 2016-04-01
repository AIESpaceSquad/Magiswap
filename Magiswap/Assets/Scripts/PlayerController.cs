using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{ 

    Rigidbody2D rigidBody;
    Vector2 jump;
    bool isGrounded;
    public int jumpForce;
    public int movementForce;
    public string controllerName;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    KeyCode spacebar = KeyCode.Space;

    static int nextPlayer = 1;

    // Use this for initialization
    void Start ()
    {

        if (nextPlayer == 1)
        {
            //grab right controller
            controllerName = CharacterSelector.rightController;
            nextPlayer++;
        }
        else if (nextPlayer == 2)
        {
            //grab left controller
            controllerName = CharacterSelector.leftController;
            nextPlayer++;
        }
        else
        {
            Destroy(gameObject);//have too many players
        }

        Debug.Log(controllerName);

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
        if (controllerName == "kbo_")
        {
            OnUpLadder();
            OnJump();
            return OnMoveLeftRight();
        }

        if (controllerName == "gp2_" || controllerName == "gp1_")
        {
            return OnMoveXstick(); 
        }


        return 0;
    }

    float OnMoveXstick()
    {
        Debug.Log("got into moveXstick");
        if (Input.GetAxis(controllerName + "rStickX") > 0|| Input.GetAxis(controllerName + "rStickX") < 0)
        {
            Debug.Log("Thou Shall MOve!");
            return Input.GetAxis(controllerName + "rStickX") * Time.deltaTime;
        }
        return 0;
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



