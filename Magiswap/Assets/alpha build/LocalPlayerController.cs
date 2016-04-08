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

    float groundRadius = 0.1f;

    bool isFacingRight = true;
    bool isGrounded = true;

    Rigidbody2D myRigidbody;

    [SerializeField]
    InventoryNode.NodeProperty accessSlot;
    InventoryGrid activeGrid;
    InventoryNode mySlot;

    float grabRadius = 1.0f;
    bool jumpRequested = false;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();

        activeGrid = FindObjectOfType<InventoryGrid>();

        mySlot = activeGrid.GetSpecialSlot(accessSlot);
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
        if (Input.GetButtonDown(controllerName + "_jump"))
        {
            jumpRequested = true;
        }

        Collider2D[] itemsInRange;
        if (Input.GetButtonDown(controllerName + "_activate"))
        {
            itemsInRange = Physics2D.OverlapCircleAll(frontCheck.transform.position, grabRadius);
            if (mySlot.item == null)
            {
                for (int i = 0; i < itemsInRange.Length; i++)
                {
                    if (itemsInRange[i].GetComponent<Item>())
                    {
                        mySlot.item = itemsInRange[i].gameObject;
                        itemsInRange[i].gameObject.SetActive(false);
                        break;
                    }
                }
            }
            else
            {
                Activateable foundItem = null;
                for (int i = 0; i < itemsInRange.Length; i++)
                {
                    foundItem = itemsInRange[i].GetComponent<Activateable>();
                    if (foundItem != null)
                    {
                        break;
                    }
                }

                if (foundItem == null)
                {
                    mySlot.item.transform.position = frontCheck.transform.position;
                    mySlot.item.SetActive(true);
                    mySlot.item = null;
                }
                else
                {
                    foundItem.AttemptActivate(mySlot.item.GetComponent<Item>());
                }
            }
        }

        if (Input.GetButtonDown(controllerName + "_swap"))
        {
            activeGrid.Swap(false);
        }
        else if (Input.GetButtonDown(controllerName + "_swapAlt"))
        {
            activeGrid.Swap(true);
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
