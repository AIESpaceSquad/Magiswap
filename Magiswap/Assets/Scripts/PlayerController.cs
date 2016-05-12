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
    float lastDirection;

    InventoryGrid grid;
    InventoryNode playerNode;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    KeyCode spacebar = KeyCode.Space;

    static int nextPlayer = 1;

    // Use this for initialization
    void Start ()
    {
        grid = GameObject.Find("InventoryGrid").GetComponent<InventoryGrid>();
        playerNode = grid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1);
        if (nextPlayer == 1)
        {
            //grab right controller
            playerNode = grid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1);
            Camera.main.transform.parent = transform;
            Camera.main.transform.position = new Vector3(transform.position.x, 4,-10);
            controllerName = CharacterSelector.rightController;
            nextPlayer++;
        }
        else if (nextPlayer == 2)
        {
            //grab left controller
            playerNode = grid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player2);
            controllerName = CharacterSelector.leftController;
            nextPlayer++;
        }
        else
        {
            Destroy(gameObject);//have too many players
        }

        //Debug.Log(controllerName);

        rigidBody = GetComponent<Rigidbody2D>();
        jump = new Vector3(0, jumpForce);
        isGrounded = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //isGrounded = true;
	}

    void FixedUpdate()
    {
        OnItemInteraction();
    }

    void OnItemInteraction()
    {
        if (Input.GetButtonDown((controllerName + "activate")))
        {
            //drop item
            if (playerNode.item != null)
            {
                Debug.Log("Drop MoFo!!");

                //set obj position next to the player
                playerNode.item.transform.position =
                    new Vector3(transform.position.x + lastDirection,
                                transform.position.y,
                                transform.position.z); 
                
                playerNode.item.SetActive(true);
                playerNode.item = null;
                return;
            }
            //Debug.Log("Direction player is facing: " + new Vector2(lastDirection, 0));
            //pick up item from ray cast
            RaycastHit2D[] hits =
                Physics2D.CircleCastAll(new Vector2(transform.position.x, transform.position.y),
                                        2,
                                        new Vector2(lastDirection, 0),
                                        1);
                //Physics2D.CircleCastAll(new Vector2(transform.position.x,      //origin
                //                                 transform.position.y), 
                //                     new Vector2(lastDirection, 0),         //Direction
                //                     4);                                    //distance
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 2, 0), 
                           new Vector3(lastDirection, 0,0), Color.red, 10);

            //Debug.Log("Find Item To Pick Up: " + hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                
                if (hits[i].transform.tag == "Item")
                {
                    Debug.Log("Interact!!");
                    playerNode.item = hits[i].transform.gameObject;
                    playerNode.item.SetActive(false);
                }
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag == "Ground")
            isGrounded = true;
    }

    public Vector3 PlayerInput()
    {
        if (controllerName == "kbo_")
        {
            OnUpLadder();
            OnJump();
            return new Vector3(OnMoveLeftRight(), 0,0);
        }

        if (controllerName == "gp2_" || controllerName == "gp1_")
        {
            //OnItemInteraction();
            OnControllerJump();
            return new Vector3(OnMoveXstick(), 0, 0); 
        }


        return new Vector3(0,0,0);
    }

    void OnControllerJump()
    {
        if(Input.GetButtonDown(controllerName + "jump") && isGrounded)
        {
            isGrounded = false;
            rigidBody.AddForce(jump);
            //return jumpForce * Time.deltaTime;
        }
        //return 0;
    }

    float OnMoveXstick()
    {
        //Debug.Log("got into moveXstick");
        if (Input.GetAxis(controllerName + "moveX") > 0|| Input.GetAxis(controllerName + "moveX") < 0)
        {
            //Debug.Log("Thou Shall MOve!");
            lastDirection = Input.GetAxis(controllerName + "moveX");
            return lastDirection * Time.deltaTime * movementForce;
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



