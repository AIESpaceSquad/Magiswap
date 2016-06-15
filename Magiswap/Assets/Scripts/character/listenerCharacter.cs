﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class listenerCharacter : MonoBehaviour {


    //[SerializeField]
    public int playerNumber = 1;
    //[SerializeField]
    public int controllerNumber = -1;//-1 is no controller

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
    bool moveLock = false;

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

    //bool isFacingRight = true;
    //bool isGrounded = true;
    bool jumpRequested = false;
    int moveDirection = 0;

    bool onMovingPlatform = false;
    Transform detectedMovingPlatform = null;
    Vector3 lastPlatformPosition;

    Rigidbody2D myRigidbody;

    static List<Collider2D> playerColiders;
    Animator myAnimator;

    public bool IsGrounded
    {
        get;
        private set;
    } 
    public bool IsFacingRight
    {
        get;
        private set;
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
        raycastHit = new RaycastHit2D[5];

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

        myAnimator = GetComponentInChildren<Animator>();

        IsFacingRight = true;
        IsGrounded = true;
    }

    // Update is called once per frame
    void FixedUpdate() {

        int[] checkResult = viewTerrianCheckpoints();

        //determine ground state
        if (checkResult[(int)TerrainCheck.feetCentre] + checkResult[(int)TerrainCheck.feetRight] + checkResult[(int)TerrainCheck.feetLeft] > 0)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        //check jump input
        if (IsGrounded && jumpRequested)
        {
            jumpTime = jumpLength;
            jumpRequested = false;
        }

        //determine velocity in the y direction
        float yVelocity = Physics2D.gravity.y;
        
        if (jumpTime > 0.0f)
        {
            if (jumpTime < jumpLength * 0.6f)//don't interupt the jump for the first part of it
            {
                if (IsGrounded)
                {
                    jumpTime = 0;
                    yVelocity = 0;
                }
            }
            if (checkResult[(int)TerrainCheck.head] > 0)
            {
                jumpTime = 0;
            }
            yVelocity = JumpCurve.Evaluate(1.0f - jumpTime / jumpLength) * maxJumpForce;
        }
        else if (IsGrounded)
        {
            yVelocity = 0;
        }

        //horisontal movement
        if (moveLock)
        {
            myRigidbody.velocity = new Vector2(0, yVelocity);
        }
        else if (IsGrounded)
        {
            float xVelocity = moveDirection * moveSpeed;
            if (xVelocity > 0.001 || xVelocity < -0.001)
            {
                if (checkResult[(int)TerrainCheck.feetLeft] > checkResult[(int)TerrainCheck.feetRight])
                {
                    RaycastHit2D hitAngle = Physics2D.Linecast(groundCheck.transform.position, groundCheck.transform.position + new Vector3(-groundXoffset, -groundRadius, 0), layermask);
                    myRigidbody.velocity = new Vector2(xVelocity, yVelocity) + (hitAngle.normal * -4);
                }
                else if (checkResult[(int)TerrainCheck.feetLeft] < checkResult[(int)TerrainCheck.feetRight])
                {
                    RaycastHit2D hitAngle = Physics2D.Linecast(groundCheck.transform.position, groundCheck.transform.position + new Vector3(groundXoffset, -groundRadius, 0), layermask);
                    myRigidbody.velocity = new Vector2(xVelocity, yVelocity) + (hitAngle.normal * -4);
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
            myRigidbody.velocity = new Vector2(moveDirection * (moveSpeed * airControll), yVelocity);
        }

        if (onMovingPlatform)
        {
            Vector3 platformDiff = detectedMovingPlatform.position - lastPlatformPosition;
            lastPlatformPosition = new Vector3() + detectedMovingPlatform.position; //being paranoid about references

            transform.position += platformDiff;
        }

        if (IsGrounded)
        {
            if ((moveDirection > 0 && !IsFacingRight) ||
                (moveDirection < 0 && IsFacingRight))
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
                    if (IsGrounded)
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
            if (jumpTime > jumpLength * 0.8f)
            {
                moveLock = true;
            }
            else
            {
                moveLock = false;
            }
                jumpTime -= Time.deltaTime;
        }
        else
        {
            moveLock = false;
        }

        //animation stuff
        myAnimator.SetBool("IsGrounded", IsGrounded);
        myAnimator.SetBool("IsJumping", (jumpTime > 0.0f));
        myAnimator.SetBool("IsRunning", !(moveDirection < 0.001 && moveDirection > -0.001));
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
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

    enum TerrainCheck
    {
        head = 0,
        feetRight = 1,
        feetLeft = 2,
        feetCentre = 3,
    }

    //array will always be length 4 in the order of head, feetRight, feetCentre, feetLeft
    int[] viewTerrianCheckpoints()
    {
        int[] results = new int[4];

        raycastHit = new RaycastHit2D[5];//clear the raycasthit array 'cuz linecast won't 
        int currentResult = Physics2D.LinecastNonAlloc(headCheck.transform.position, headCheck.transform.position + new Vector3(0, groundRadius * 3, 0), raycastHit, layermask);
        results[(int)TerrainCheck.head] = numOfNonPlayerColliders(raycastHit, currentResult);

        raycastHit = new RaycastHit2D[5];
        currentResult = Physics2D.LinecastNonAlloc(groundCheck.transform.position + new Vector3(groundXoffset, 0, 0), groundCheck.transform.position + new Vector3(groundXoffset, -groundRadius, 0), raycastHit, layermask);
        results[(int)TerrainCheck.feetRight] = numOfNonPlayerColliders(raycastHit, currentResult);

        raycastHit = new RaycastHit2D[5];
        currentResult = Physics2D.LinecastNonAlloc(groundCheck.transform.position + new Vector3(-groundXoffset, 0, 0), groundCheck.transform.position + new Vector3(-groundXoffset, -groundRadius, 0), raycastHit, layermask);
        results[(int)TerrainCheck.feetLeft] = numOfNonPlayerColliders(raycastHit, currentResult);

        raycastHit = new RaycastHit2D[5];
        currentResult = Physics2D.LinecastNonAlloc(groundCheck.transform.position, groundCheck.transform.position + new Vector3(0, -groundRadius, 0), raycastHit, layermask);
        results[(int)TerrainCheck.feetCentre] = numOfNonPlayerColliders(raycastHit, currentResult);

        bool wasOnMoovingPatform = onMovingPlatform;
        onMovingPlatform = false;

        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].transform != null && raycastHit[i].transform.tag.CompareTo("MovingPlatform") == 0)
            {
                onMovingPlatform = true;
                if (!wasOnMoovingPatform)
                {
                    detectedMovingPlatform = raycastHit[i].transform;
                    lastPlatformPosition = new Vector3() + detectedMovingPlatform.position;//making sure that i'm not just taking a refrence
                    break;
                }
            }
        }

        //results[(int)TerrainCheck.head]       = Physics2D.LinecastNonAlloc(headCheck.transform.position, headCheck.transform.position + new Vector3(0, groundRadius * 3, 0), raycastHit, layermask);
        //results[(int)TerrainCheck.feetRight]  = Physics2D.LinecastNonAlloc(groundCheck.transform.position + new Vector3(groundXoffset, 0, 0), groundCheck.transform.position + new Vector3(groundXoffset, -groundRadius, 0), raycastHit, layermask);
        //results[(int)TerrainCheck.feetLeft]   = Physics2D.LinecastNonAlloc(groundCheck.transform.position + new Vector3(-groundXoffset, 0, 0), groundCheck.transform.position + new Vector3(-groundXoffset, -groundRadius, 0), raycastHit, layermask);
        //results[(int)TerrainCheck.feetCentre] = Physics2D.LinecastNonAlloc(groundCheck.transform.position, groundCheck.transform.position + new Vector3(0, -groundRadius, 0), raycastHit, layermask);

        return results;

    }

    int numOfNonPlayerColliders(RaycastHit2D[] in_colliders, int in_originalCount)
    {
        int count = in_originalCount;
        for (int i = 0; i < in_colliders.Length; i++)
        {
            bool isPlayerCollider = false;
            for (int j = 0; j < playerColiders.Count; j++)
            {
                if (in_colliders[i].collider == playerColiders[j])
                {
                    isPlayerCollider = true;
                }
            }

            if (isPlayerCollider)
            {
                count--;
            }
        }

        return count;
    }

    void OnDestroy()
    {
        playerColiders = new List<Collider2D>();
    }
}
