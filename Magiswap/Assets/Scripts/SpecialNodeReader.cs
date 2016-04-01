using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

[System.Serializable]
public class ChangeEvent : UnityEvent<ColorManager.CollisionColor>
{

}

public class SpecialNodeReader : MonoBehaviour {

    [SerializeField]
    InventoryNode.NodeProperty subscribedNode;

    [SerializeField]
    bool autoChangeColor = true;
    [SerializeField]
    bool autoChangeUseActiveWhite = false;
    //all subscribed items should assume they start at cc_StaticWhite.// both cc_StaticWhite & cc_ActiveWhite will be crushed into cc_StaticWhite when this meathod is called, handle this yourself.
    public ChangeEvent CalledOnChange;

    InventoryNode trackedNode;
    ColorManager.CollisionColor lastColor;

	// Use this for initialization
	void Start () {
        InventoryGrid trackedGrid = GameObject.FindObjectOfType<InventoryGrid>();
        if (trackedGrid == null)
        {
            Debug.Log("Inventory grid not found, SpecialNodeReader will not function as intended");
        }

        trackedNode = trackedGrid.GetSpecialSlot(subscribedNode);

        if (trackedNode == null)
        {
            Debug.Log("Subscribed node not found, SpecialNodeReader will not function as intended");
        }

        //while we will only send out cc_StaticWhite the items we are comparing will always be cc_ActiveWhite so we will keep it internaly as the latter
        lastColor = ColorManager.CollisionColor.cc_ActiveWhite;

        if (autoChangeColor)
        {
            if (autoChangeUseActiveWhite)
            {
                ColorManager.ChangeColor(gameObject, ColorManager.CollisionColor.cc_ActiveWhite);
            }
            else
            {
                ColorManager.ChangeColor(gameObject, ColorManager.CollisionColor.cc_StaticWhite);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        ColorManager.CollisionColor currentColor = lastColor;
        if (trackedNode != null)
        {
            if (trackedNode.item == null)
            {
                currentColor = ColorManager.CollisionColor.cc_ActiveWhite;
            }
            else
            {
                currentColor = ColorManager.GetColor(trackedNode.item);
            }
        }

        if (currentColor != lastColor)
        {
            lastColor = currentColor;

            if (autoChangeColor && autoChangeUseActiveWhite)
            {
                ColorManager.ChangeColor(gameObject, currentColor);
            }

            if (currentColor == ColorManager.CollisionColor.cc_ActiveWhite)
            {
                currentColor = ColorManager.CollisionColor.cc_StaticWhite;
            }
            
            if (autoChangeColor && !autoChangeUseActiveWhite)
            {
                ColorManager.ChangeColor(gameObject, currentColor);
            }

            if (CalledOnChange != null)
                CalledOnChange.Invoke(currentColor);
        }
	}

    void ChangeSubscribedNode(InventoryNode.NodeProperty in_newTrackedNode)
    {
        InventoryGrid trackedGrid = GameObject.FindObjectOfType<InventoryGrid>();
        trackedNode = trackedGrid.GetSpecialSlot(subscribedNode);

        if (trackedNode == null)
        {
            Debug.Log("Subscribed node not found, SpecialNodeReader will not function as intended");
        }
    }
}
