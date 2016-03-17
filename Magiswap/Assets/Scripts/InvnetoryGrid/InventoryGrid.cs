using UnityEngine;
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

    class InvnetoryNode
    {
        public int nodeName;
        public GameObject item; //sould be disabled at all times when in inventory
        public float offsetX;
        public float offsetY;
        public NodeProperty property;
        public int linkA;
        public int linkB;
    }

    List<InvnetoryNode> nodes;

	// Use this for initialization
	void Start () {
        if (template == null)
        {
            Debug.Log("InventoryGrid has run without a template and will not function as intended");
        }
        BuildFromTemplate();
	}

    void BuildFromTemplate()
    {
        nodes = new List<InvnetoryNode>();

        for (int i = 0; i < template.nodes.Count; i++)
        {
            nodes.Add(new InvnetoryNode());
            nodes[i].nodeName = i;
            nodes[i].property = template.nodes[i].property;
            nodes[i].offsetX = template.nodes[i].ofsetX;
            nodes[i].offsetY = template.nodes[i].ofsetY;
            nodes[i].linkA = template.nodes[i].linkA;
            nodes[i].linkB = template.nodes[i].linkB;
            nodes[i].item = null;
        }
    }

}
