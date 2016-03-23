﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryGrid : MonoBehaviour {

    [SerializeField]
    InventoryTemplate template;

    public enum NodeProperty
    {
        np_None = 0,
        np_Player1,
        np_Player2,
        np_Symbol1,
        np_Symbol2,
        np_Symbol3
    }

    public class InventoryNode
    {
        public int nodeName;
        public GameObject item; //sould be disabled at all times when in inventory
        //public bool isEmpty;
        //public int item;
        public float offsetX;
        public float offsetY;
        public NodeProperty property;
        public int linkA;
        public int linkB;
    }
    [HideInInspector]
    public List<InventoryNode> nodes;

	// Use this for initialization
	void Awake () {
        if (template == null)
        {
            Debug.Log("InventoryGrid has run without a template and will not function as intended");
        }
        BuildFromTemplate();
	}

    public void Swap(bool useAltSwap = false)
    {
        List<GameObject> oldItems = new List<GameObject>();
        for (int i = 0; i < nodes.Count; i++)
        {
            oldItems.Add(nodes[i].item);
        }

        for (int i = 0; i < oldItems.Count; i++)
        {
            if (useAltSwap)
            {
                if (nodes[i].linkB < 0 || nodes[i].linkB > nodes.Count)
                {
                    break;
                } 
                nodes[nodes[i].linkB].item = oldItems[i];
            }
            else
            {
                if (nodes[i].linkA < 0 || nodes[i].linkA > nodes.Count)
                {
                    break;
                }
                nodes[nodes[i].linkA].item = oldItems[i];
            }
        }
    }

    //This method returns null fi item is not found
    public InventoryNode GetSpecialSlot(NodeProperty in_specialSlot)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].property == in_specialSlot)
            {
                return nodes[i];
            }
        }

        return null;
    }


    void BuildFromTemplate()
    {
        nodes = new List<InventoryNode>();

        for (int i = 0; i < template.nodes.Count; i++)
        {
            nodes.Add(new InventoryNode());
            nodes[i].nodeName = i;
            nodes[i].property = template.nodes[i].property;
            nodes[i].offsetX = template.nodes[i].ofsetX;
            nodes[i].offsetY = template.nodes[i].ofsetY;
            nodes[i].linkA = template.nodes[i].linkA;
            nodes[i].linkB = template.nodes[i].linkB;
            nodes[i].item = null;
            //nodes[i].isEmpty = true;
            //nodes[i].item = i + 1;
        }
    }

}
