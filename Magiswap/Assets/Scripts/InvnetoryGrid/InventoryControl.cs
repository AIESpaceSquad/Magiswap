using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class InventoryModifyEvent : UnityEvent<int, bool>//player (if -1 then it's a swap), isDrop (alteritively isAltSwap)
{

}

public class InventoryControl : MonoBehaviour {

    static InventoryGrid activeGrid;
    static InventoryNode player1Item;
    static InventoryNode player2Item;

    [SerializeField]
    static float majorActionCooldown = 0.5f;
    [SerializeField]
    static float minorActionCooldown = 0.25f;

    static float remainingCooldown = 0.0f;

    public static float SwapCooldown
    {
        get
        {
            return majorActionCooldown;
        }
    }
    public static float DropCooldown
    {
        get
        {
            return minorActionCooldown;
        }
    }
    public static float RemainingCooldown
    {
        get {
            return remainingCooldown;
        }
    }

    [SerializeField]
    InventoryModifyEvent CalledBeforeChange;
    [SerializeField]
    InventoryModifyEvent CalledAfterChange;

    static InventoryModifyEvent staticBeforeChange;
    static InventoryModifyEvent staticAfterChange;

	// Use this for initialization
	void Start () {
        if (activeGrid != null)
        {
            Destroy(gameObject);
            return;
        }
        activeGrid = GetComponent<InventoryGrid>();
        player1Item = activeGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1);
        player2Item = activeGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player2);

        staticBeforeChange = CalledBeforeChange;
        staticAfterChange = CalledAfterChange;
	}
	
	// Update is called once per frame
	void Update () {
	    if (remainingCooldown > 0.0f)
        {
            remainingCooldown -= Time.deltaTime;
        }
	}

    //returns false if the cooldown has not elapsed
    static public bool RequestSwap(bool in_alternate = false)
    {
        if (remainingCooldown > 0.0f)
        {
            return false;
        }

        staticBeforeChange.Invoke(-1, in_alternate);

        activeGrid.Swap(in_alternate);
        remainingCooldown = majorActionCooldown;

        staticAfterChange.Invoke(-1, in_alternate);
        return true;
    }

    //returns false if the player has no tiem or the input is invalid
    static public bool HasItem(int in_player)
    {
        if (in_player == 1)
        {
            return (player1Item.item != null);
        }
        else if (in_player == 2)
        {
            return (player2Item.item != null);
        }
        Debug.Log("HasItem: received input was invalid");
        return false;
    }

    //return -2 if the item does not exist or the input is invalid, -1 if the item has no key or the key ID if there is one
    static public int RequestItemKey(int in_player)
    {
        if (!HasItem(in_player))//if the player does not have an item
        {
            Debug.Log("RequestItemKey: player " + in_player + " has no item");
            return -2;
        }

        if (in_player == 1)
        {
            return player1Item.item.GetComponent<Item>().activatorKey;
        }
        else if (in_player == 2)
        {
            return player2Item.item.GetComponent<Item>().activatorKey;
        } 
        else
        {
            Debug.Log("RequestItemKey: received input was invalid");
            return -2;
        }
    }

    //returns null if there is no item, the cooldown has not elapsed, or the imput was invalid
    //if successfull the item is removed from the inventory slot and activated in the given position
    static public Item RequestDrop(int in_player, Vector2 in_dropLocation)
    {
        if (remainingCooldown > 0.0f)
        {
            return null;
        }

        staticBeforeChange.Invoke(in_player, true);

        GameObject currentItem;
        if (in_player == 1)
        {
            currentItem = player1Item.item;
            player1Item.item = null;
        }
        else if (in_player == 2)
        {
            currentItem = player2Item.item;
            player2Item.item = null;
        }
        else
        {
            Debug.Log("RequestDrop: in_player input was invalid");
            return null;
        }

        if (currentItem == null)//could use has item somewhere to make this more readable.
        {
            Debug.Log("RequestDrop: specified player: " + in_player + " has no item");
            return null;
        }

        currentItem.transform.position = in_dropLocation;
        Rigidbody2D currentItemRigidbody = currentItem.GetComponent<Rigidbody2D>();
        if (currentItemRigidbody != null)
        {
            currentItemRigidbody.velocity = new Vector2();
        }

        currentItem.SetActive(true);
        remainingCooldown = minorActionCooldown;

        staticAfterChange.Invoke(in_player, true);

        return currentItem.GetComponent<Item>();
    }

    //returns false if the cooldown has not elapsed, an item is already present or the input is invalid
    //if successfull the item is added to the inventoy slot and deactivated, this function will return true
    static public bool RequestPickup(int in_player, Item in_incomingItem)
    {
        if (remainingCooldown > 0.0f)
        { 
            return false;
        }

        if (HasItem(in_player))
        {
            Debug.Log("RequestPickup: given player: " + in_player + " already has an item");
            return false;
        }

        if (in_incomingItem == null)
        {
            Debug.Log("RequestPickup: given in_incomingItem is invalid");
            return false;
        }

        staticBeforeChange.Invoke(in_player, false);

        if (in_player == 1)
        {
            player1Item.item = in_incomingItem.gameObject;
        }
        else if (in_player == 2)
        {
            player2Item.item = in_incomingItem.gameObject;
        }
        else
        {
            Debug.Log("RequestPickup: in_player input was invalid");
            return false;
        }

        remainingCooldown = minorActionCooldown;
        in_incomingItem.gameObject.SetActive(false);

        staticAfterChange.Invoke(in_player, false);

        return true;

    }

    //return null if the item does not exist
    //this does not activate or move the item, use RequestDrop to do so
    static public Item RequestItemInfo(int in_player)
    {
        if (in_player == 1)
        {
            return player1Item.item.GetComponent<Item>();
        }
        else if (in_player == 2)
        {
            return player2Item.item.GetComponent<Item>();
        }
        else
        {
            Debug.Log("RequestItemInfo: Invalid input received");
            return null;
        }

    }
}
