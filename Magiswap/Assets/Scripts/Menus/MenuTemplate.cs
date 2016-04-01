using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MenuNode
{
    [System.Serializable]
    public struct NodeItem
    {
        public enum ItemAction
        {
            ia_NotActive = -1, //Item will not be displayed
            ia_DoNothing = 0,
            ia_ChangeNode, //Changes to the node named in the item's parameter
        }

        public string itemText;
        public ItemAction itemActions;
        public string itemParams;
    }

    public string nodeName;
    public NodeItem[] nodeItems;

}

[CreateAssetMenu(fileName = "NewMenu", menuName = "Magiswap/MenuTemplate", order = 2)]
public class MenuTemplate : ScriptableObject
{
    public string rootMenu;
    public List<MenuNode> SubMenus;

}
