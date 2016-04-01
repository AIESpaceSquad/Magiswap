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
            ia_DoNothing = 0,
            ia_ChangeNode, //Changes to the node named in the item's parameter
            ia_Function0,
            ia_Function1,
            ia_Function2,
            ia_Function3,
            ia_Function4,
            ia_Function5,
            ia_Function6,
        }

        public string itemText;
        public ItemAction itemActions;
        public string itemParams;
    }

    public string nodeName;
    public string returnNode;
    public NodeItem[] nodeItems;
    public bool hideTitle;

}

[CreateAssetMenu(fileName = "NewMenu", menuName = "Magiswap/MenuTemplate", order = 2)]
public class MenuTemplate : ScriptableObject
{
    public string rootMenu;
    public List<MenuNode> SubMenus;

}
