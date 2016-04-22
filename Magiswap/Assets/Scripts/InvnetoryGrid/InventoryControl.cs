using UnityEngine;
using System.Collections;

public class InventoryControl : MonoBehaviour {

    InventoryGrid activeGrid;
    InventoryNode player1Item;
    InventoryNode player2Item;

    [SerializeField]
    float majorActionCooldown = 1.0f;
    [SerializeField]
    float minorActionCooldown = 0.25f;

    float remainingCooldown = 0.0f;

	// Use this for initialization
	void Start () {
        activeGrid = GetComponent<InventoryGrid>();
        player1Item = activeGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1);
        player2Item = activeGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player2);
	}
	
	// Update is called once per frame
	void Update () {
	    if (remainingCooldown > 0.0f)
        {
            remainingCooldown -= Time.deltaTime;
        }
	}

    //returns false if the cooldown has not elapsed
    public bool RequestSwap(bool in_alternate = false)
    {
        if (remainingCooldown >= 0.0f)
        {
            return false;
        }
        activeGrid.Swap(in_alternate);
        remainingCooldown = majorActionCooldown;
        return true;
    }

    //returns false if the player has no tiem or the input is invalid
    public bool HasItem(int in_player)
    {
        return false;
    }

    //return -2 if the item does not exist or the input is invalid, -1 if the item has no key or the key ID if there is one
    public int RequestItemKey(int in_player)
    {
        return 0;
    }

    //returns null if there is no item, the cooldown has not elapsed, or the imput was invalid
    //if successfull the item is removed from the inventory slot and activated in the given position
    public Item RequestDrop(int in_player, Vector2 in_dropLocation)
    {
        return null;
    }

    //returns false if the cooldown has not elapsed, an item is already present or the input is invalid
    //if successfull the item is added to the inventoy slot and deactivated
    public bool RequestPickup(int in_player, Item in_incomingItem)
    {
        return false;
    }

    //return null if the item does not exist
    //this does not activate or move the item, use RequestDrop to do so
    public Item RequestItemInfo(int in_player)
    {
        return null;
    }
}
