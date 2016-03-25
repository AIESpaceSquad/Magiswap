using UnityEngine;
using System.Collections;


[System.Serializable]
public class InventoryNode
{

    public enum NodeProperty
    {
        np_None = 0,
        np_Player1,
        np_Player2,
        np_Symbol1,
        np_Symbol2,
        np_Symbol3
    }

    [System.NonSerialized]
    public GameObject item; //sould be disabled at all times when in inventory
                            //public bool isEmpty;
                            //public int item;
    public float offsetX;
    public float offsetY;
    public NodeProperty property;
    public int linkA;
    public int linkB;
}