using UnityEngine;
using System.Collections;

public class platformColorChanger : MonoBehaviour {

    [SerializeField]
    Material changedMaterial;
    [SerializeField]
    InventoryGrid readGrid;
    [SerializeField]
    InventoryNode.NodeProperty readNode = InventoryNode.NodeProperty.np_Symbol1;

    InventoryNode cachedNode;

	// Use this for initialization
	void Start () {
        cachedNode = readGrid.GetSpecialSlot(readNode);

	}
	
	// Update is called once per frame
	void Update () {
	    if (cachedNode.item == null)
        {
            changedMaterial.color = Color.white;
        }
        else
        {
            changedMaterial.color = ColorManager.GetActualColor(cachedNode.item);
        }
	}
}
