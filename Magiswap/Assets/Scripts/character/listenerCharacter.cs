using UnityEngine;
using System.Collections;

public class listenerCharacter : MonoBehaviour {

    [SerializeField]
    int playerNumber = 1;
    [SerializeField]
    int controllerNumber = -1;//-1 is no controller

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

    float groundRadius = 0.1f;
    float grabRadius = 1.0f;

    bool isFacingRight = true;
    bool isGrounded = true;
    bool jumpRequested = false;
    int moveDirection = 0;

    Rigidbody2D myRigidbody;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    } 
    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
    public float JumpForce
    {
        get
        {
            return jumpForce;
        }
    }

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

        if (isGrounded && jumpRequested)
        {
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            jumpRequested = false;
        }

        if (isGrounded)
        {
            myRigidbody.velocity = new Vector2(moveDirection * moveSpeed, myRigidbody.velocity.y);
        }
        else
        {
            //we'll stick some starting lag in here to make the speed difference less abrupt
            myRigidbody.velocity = new Vector2(moveDirection * (moveSpeed / 3), myRigidbody.velocity.y);
        }

        if (isGrounded)
        {
            if ((moveDirection > 0 && !isFacingRight) ||
                (moveDirection < 0 && isFacingRight))
            {
                Flip();
            }
        }
    }

    void Update()
    {
        if (controllerNumber != -1)
        {
            switch (ControllerHandler.GetControllerMovementState(controllerNumber))
            {
                case InputTranslator.StateCode.state_mov_left:
                    moveDirection = -1;
                    break;
                case InputTranslator.StateCode.state_mov_right:
                    moveDirection = 1;
                    break;
                default:
                    moveDirection = 0;
                    break;
            }

            switch (ControllerHandler.GetControllerAcionState(controllerNumber))
            {
                case InputTranslator.StateCode.state_act_jump:
                    if (isGrounded)
                    {
                        jumpRequested = true;
                        ControllerHandler.ActionFulfilled(controllerNumber);
                    }
                    break;
                case InputTranslator.StateCode.state_act_swap_pri:
                    InventoryControl.RequestSwap(false);
                    ControllerHandler.ActionFulfilled(controllerNumber);
                    break;
                case InputTranslator.StateCode.state_act_swap_alt:
                    InventoryControl.RequestSwap(true);
                    ControllerHandler.ActionFulfilled(controllerNumber);
                    break;
                case InputTranslator.StateCode.state_act_activate:

                    Collider2D[] itemsInRange = Physics2D.OverlapCircleAll(frontCheck.transform.position, grabRadius);

                    if (!InventoryControl.HasItem(playerNumber))
                    {
                        for (int i = 0; i < itemsInRange.Length; i++)
                        {
                            if (itemsInRange[i].GetComponent<Item>())
                            {
                                InventoryControl.RequestPickup(playerNumber, itemsInRange[i].GetComponent<Item>());
                            }
                        }
                    }
                    else
                    {
                        Activateable foundObject = null;
                        for (int i = 0; i < itemsInRange.Length; i++)
                        {
                            foundObject = itemsInRange[i].GetComponent<Activateable>();
                            if (foundObject != null)
                            {
                                break;
                            }
                        }

                        if (foundObject == null)
                        {
                            InventoryControl.RequestDrop(playerNumber, frontCheck.transform.position);
                        }
                        else
                        {
                            foundObject.AttemptActivate(InventoryControl.RequestItemInfo(playerNumber));
                        }
                    }
                    ControllerHandler.ActionFulfilled(controllerNumber);

                    break;
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }


}
