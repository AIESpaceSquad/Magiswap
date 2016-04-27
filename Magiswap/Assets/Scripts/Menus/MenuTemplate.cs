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
            ia_ExitMenu,
            ia_ExitGame,
            ia_OpenOptionsMenu,
            ia_OpenControllerRegistry,
            ia_OpenNetworkMatchmaker,
            ia_OpenGameInitalizer,
            ia_ReturnToMain,
            ia_RevertGameCheckpoint,
            ia_RevertGameLevel,
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
