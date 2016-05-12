﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class listenerCharacter : MonoBehaviour {


    [SerializeField]
    int playerNumber = 1;
    [SerializeField]
    int controllerNumber = -1;//-1 is no controller

    [SerializeField]
    float moveSpeed = 10;
    [SerializeField]
    float maxJumpForce = 15;
    [SerializeField]
    float jumpLength = 2.0f;
    [SerializeField]
    AnimationCurve JumpCurve;
    [SerializeField]
    float airControll = 0.4f;
    float jumpTime = 0.0f;

    RaycastHit2D[] raycastHit;
    [SerializeField]
    GameObject groundCheck;
    [SerializeField]
    GameObject frontCheck;
    [SerializeField]
    GameObject headCheck;
    [SerializeField]
    LayerMask deafultLayermask;
    LayerMask layermask;

    float groundRadius = 0.01f;
    float groundXoffset = 0.5f;
    float grabRadius = 1.0f;

    bool isFacingRight = true;
    bool isGrounded = true;
    bool jumpRequested = false;
    int moveDirection = 0;

    Rigidbody2D myRigidbody;

    static List<Collider2D> playerColiders;

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
            return maxJumpForce;
        }
    }

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        UpdateLayermask(ColorManager.CollisionColor.cc_ActiveWhite);
        raycastHit = new RaycastHit2D[1];

        if (playerColiders == null)
        {
            playerColiders = new List<Collider2D>();
        }

        for (int i = 0; i < playerColiders.Count; i++)
        {
            Physics2D.IgnoreCollision(playerColiders[i], GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(playerColiders[i], GetComponent<CircleCollider2D>());
        }
        playerColiders.Add(GetComponent<BoxCollider2D>());
        playerColiders.Add(GetComponent<CircleCollider2D>());
    }

    // Update is called once per frame
    void FixedUpdate() {
        //run terrain checkss
        int headResult = Physics2D.LinecastNonAlloc(headCheck.transform.position, headCheck.transform.position + new Vector3(0, groundRadius * 3, 0), raycastHit, layermask);
        int rightResult = Physics2D.LinecastNonAlloc(groundCheck.transform.position + new Vector3(groundXoffset, 0, 0), groundCheck.transform.position + new Vector3(groundXoffset, -groundRadius, 0), raycastHit, layermask);
        int leftResult = Physics2D.LinecastNonAlloc(groundCheck.transform.position + new Vector3(-groundXoffset, 0, 0), groundCheck.transform.position + new Vector3(-groundXoffset, -groundRadius, 0), raycastHit, layermask);
        int midResult = Physics2D.LinecastNonAlloc(groundCheck.transform.position, groundCheck.transform.position + new Vector3(0, -groundRadius, 0), raycastHit, layermask);

        //make the raycasthitarray bigger
        //make a method that runs through raycast hit and reduces the number of hits if any of the hits are items in the playerColiders list
        //call this on each result so we dont think we are in the groud while inside a player

        //determine ground state
        if (midResult + rightResult + leftResult != 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //check jump input
        if (isGrounded && jumpRequested)
        {
            jumpTime = jumpLength;
            jumpRequested = false;
        }

        //determine velocity in the y direction
        float yVelocity = Physics2D.gravity.y;
        if (jumpTime < jumpLength * 0.6f)//don't interupt the jump for the first part of it
        {
            if (isGrounded)
            {
                jumpTime = 0;
                yVelocity = 0;
            }
        }
        else if (jumpTime > 0.0f)
        {
            if (headResult > 0)
            {
                jumpTime = 0;
            }
            yVelocity = JumpCurve.Evaluate(1.0f - jumpTime / jumpLength) * maxJumpForce;
        }
        else if (isGrounded)
        {
            yVelocity = 0;
        }

        //horisontal movement
        if (isGrounded)
        {
            float xVelocity = moveDirection * moveSpeed;
            if (xVelocity > 0.001 || xVelocity < -0.001)
            {
                if (leftResult != rightResult)
                {
                    myRigidbody.velocity = new Vector2(xVelocity, yVelocity) + (raycastHit[0].normal * -7);
                }
                else
                {
                    myRigidbody.velocity = new Vector2(xVelocity, yVelocity);
                }
            }
            else
            {
                myRigidbody.velocity = new Vector2(0, yVelocity);
            }
        }
        else
        {
            //we'll stick some starting lag in here to make the speed difference less abrupt
            myRigidbody.velocity = new Vector2(moveDirection * (moveSpeed * airControll), yVelocity);
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
        //get input 
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

            //interpret input
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
                        bool pickupResult = false;
                        for (int i = 0; i < itemsInRange.Length; i++)
                        {
                            if (itemsInRange[i].GetComponent<Item>())
                            {
                                pickupResult = InventoryControl.RequestPickup(playerNumber, itemsInRange[i].GetComponent<Item>());
                            }
                            if (pickupResult == true)
                            {
                                break;
                            }
                        }
                        
                        if (pickupResult == false)
                        {
                            Activateable foundObject = null;
                            for (int i = 0; i < itemsInRange.Length; i++)
                            {
                                foundObject = itemsInRange[i].GetComponent<Activateable>();
                                if (foundObject != null)
                                {
                                    bool result = foundObject.AttemptActivate(null);
                                    if (result == true)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        foundObject = null;
                                    }
                                }
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
                                bool result = foundObject.AttemptActivate(InventoryControl.RequestItemInfo(playerNumber));
                                if (result == true)
                                {
                                    break;//if we break here then the found object will still be filled for the drop to be able to check
                                }
                                else
                                {
                                    foundObject = null;
                                }
                            }
                        }

                        if (foundObject == null)
                        {
                            InventoryControl.RequestDrop(playerNumber, frontCheck.transform.position);
                        }
                    }
                    ControllerHandler.ActionFulfilled(controllerNumber);

                    break;
            }
        }

        if (jumpTime > 0.0f)
        {
            jumpTime -= Time.deltaTime;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    public void UpdateLayermask(ColorManager.CollisionColor in_newColor)
    {
        if (ColorManager.IsColor(gameObject, ColorManager.CollisionColor.cc_ActiveWhite))
        {
            layermask = deafultLayermask.value;
        }
        else
        {
            layermask = deafultLayermask.value | (1 << gameObject.layer);
        }
    }


}
