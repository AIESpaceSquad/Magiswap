using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullUI : MonoBehaviour {

    //Image settings
    [SerializeField]
    Sprite BGNoSpecial;
    [SerializeField]
    Sprite BGPlayer1;
    [SerializeField]
    Sprite BGPlayer2;
    [SerializeField]
    Sprite BGSymbol1;
    [SerializeField]
    Sprite BGSymbol2;
    [SerializeField]
    Sprite BGSymbol3;
    [SerializeField]
    Sprite PIPlayer1;
    [SerializeField]
    Sprite PIPlayer2;
    [SerializeField]
    Sprite PIBorder;

    //positioning settings
    [SerializeField]
    float inventoryScale;
    [SerializeField]
    float offsetSize;

    //image storage
    Image[] inventoryBackgrounds;
    GameObject[] inventoryItems;
    GameObject player1Portrait;
    GameObject player2Portrait;
    //GameObject player1Border;
    //GameObject player2Border;

    //cached Components
    Image[] inventoryItemImages;
    Image p1BorderImage;
    Image p2BorderImage;

    InventoryGrid trackedGrid;

    //transitions stuff
    Vector2[] itemRestingPositions;
    Vector2[] itemTargetPositions;
    Sprite[] itemTransitionSprites;
    int dropedItemIndex = -1;

    // Use this for initialization
    void Start () {
        RefreshTrackedGrid();
        transform.localScale = new Vector3(inventoryScale, inventoryScale, inventoryScale);
	}
	
	// Update is called once per frame
	void Update () {
	    
        for (int i = 0; i < trackedGrid.nodes.Count; i++)
        {
            //item state
            Item currentItem;
            if (trackedGrid.nodes[i].item == null)
            {
                currentItem = null;
            }
            else
            {
                currentItem = trackedGrid.nodes[i].item.GetComponent<Item>();
            }

            //BG color
            if (currentItem == null)
            {
                inventoryBackgrounds[i].color = Color.white;
                inventoryItemImages[i].color = new Color(1, 1, 1, 0);
            }
            else
            {
                inventoryBackgrounds[i].color = ColorManager.GetActualColor(currentItem.gameObject);
                inventoryItemImages[i].color = Color.white;
            }

            //item stuff
            if (InventoryControl.RemainingCooldown >= 0.0f)
            {
                if (itemTransitionSprites[i])//it's in an array and it will always exist
                {
                    //inventoryItemImages[i].color = Color.white;
                    inventoryItemImages[i].color = Color.white;
                }
                else
                {
                    inventoryItemImages[i].color = new Color(1, 1, 1, 0);
                }
                inventoryItemImages[i].sprite = itemTransitionSprites[i];
            }
            else
            {
                if (currentItem != null)
                {
                    inventoryItemImages[i].sprite = currentItem.UISprite;
                }
            }
        }

        p1BorderImage.color = ColorManager.GetActualColor(trackedGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1).item);
        p2BorderImage.color = ColorManager.GetActualColor(trackedGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player2).item);

    }

    //dosen't delete game objects probably shouldn't be used more than once
    public void RefreshTrackedGrid()
    {
        trackedGrid = FindObjectOfType<InventoryGrid>();

        if (trackedGrid == null)
        {
            Debug.Log("No grid found, UI will not work correctly");
            return;
        }

        //generate images
        inventoryBackgrounds = new Image[trackedGrid.nodes.Count];
        inventoryItems = new GameObject[trackedGrid.nodes.Count];
        inventoryItemImages = new Image[trackedGrid.nodes.Count];
        itemRestingPositions = new Vector2[trackedGrid.nodes.Count];
        itemTargetPositions = new Vector2[trackedGrid.nodes.Count];
        itemTransitionSprites = new Sprite[trackedGrid.nodes.Count];
        
        for (int i = 0; i < trackedGrid.nodes.Count; i++)
        {
            GameObject inventoryBGGameObject = new GameObject("BackgroundImage" + i);
            inventoryItems[i] = new GameObject("ItemImage" + i);

            inventoryBGGameObject.transform.parent = transform;
            inventoryItems[i].transform.parent = transform;

            //positions
            Vector3 offset = new Vector3(trackedGrid.nodes[i].offsetX, trackedGrid.nodes[i].offsetY, 0) * offsetSize;
            itemRestingPositions[i] = transform.position + offset;

            itemTargetPositions[i] = itemRestingPositions[i];
            inventoryBGGameObject.transform.position = itemRestingPositions[i];
            inventoryItems[i].transform.position = itemRestingPositions[i]; //may be unnessicary after transitions are implemented

            //images
            inventoryBGGameObject.AddComponent<Image>();
            inventoryBackgrounds[i] = inventoryBGGameObject.GetComponent<Image>();

            inventoryItems[i].AddComponent<Image>();
            inventoryItemImages[i] = inventoryItems[i].GetComponent<Image>();

            switch (trackedGrid.nodes[i].property)
            {
                case InventoryNode.NodeProperty.np_None:
                    inventoryBackgrounds[i].sprite = BGNoSpecial;
                    break;
                case InventoryNode.NodeProperty.np_Player1:
                    inventoryBackgrounds[i].sprite = BGPlayer1;
                    break;
                case InventoryNode.NodeProperty.np_Player2:
                    inventoryBackgrounds[i].sprite = BGPlayer2;
                    break;
                case InventoryNode.NodeProperty.np_Symbol1:
                    inventoryBackgrounds[i].sprite = BGSymbol1;
                    break;
                case InventoryNode.NodeProperty.np_Symbol2:
                    inventoryBackgrounds[i].sprite = BGSymbol2;
                    break;
                case InventoryNode.NodeProperty.np_Symbol3:
                    inventoryBackgrounds[i].sprite = BGSymbol3;
                    break;
            }

            inventoryItemImages[i].sprite = BGNoSpecial;
            inventoryItemImages[i].color = new Color(1, 1, 1, 0);
        }

        //portrait time
        player1Portrait = new GameObject("player1Portrait");
        //this add & use is scetchy to me
        player1Portrait.AddComponent<Image>().sprite = PIPlayer1;

        player1Portrait.transform.SetParent(transform.parent); //MY parent not myself
        player1Portrait.transform.position = transform.position + (Vector3)(trackedGrid.player1UIOffset * offsetSize);

        //p2 portrait
        player2Portrait = new GameObject("player2Portrait");

        player2Portrait.AddComponent<Image>().sprite = PIPlayer2;

        player2Portrait.transform.SetParent(transform.parent);
        player2Portrait.transform.position = transform.position + (Vector3)(trackedGrid.player2UIOffset * offsetSize);

        //borders
        GameObject player1BorderGO = new GameObject("player1Border");

        player1BorderGO.transform.SetParent(transform.parent);
        player1BorderGO.transform.position = transform.position + new Vector3(0, 0, 1) + (Vector3)(trackedGrid.player1UIOffset * offsetSize);//magic shift forward on the z so it's on top of the portrait

        p1BorderImage = player1BorderGO.AddComponent<Image>();

        //border2
        GameObject player2BorderGO = new GameObject("player2Border");

        player2BorderGO.transform.SetParent(transform.parent);
        player2BorderGO.transform.position = transform.position + new Vector3(0, 0, 1) + (Vector3)(trackedGrid.player2UIOffset * offsetSize);

        p2BorderImage = player2BorderGO.AddComponent<Image>();

        //set border images
        p1BorderImage.sprite = PIBorder;
        p2BorderImage.sprite = PIBorder;

    }

    public void CalledBeforeSwap(int in_player, bool in_parameter)
    {
        //cache item sprites
        itemTransitionSprites = new Sprite[trackedGrid.nodes.Count];
        for (int i = 0; i < trackedGrid.nodes.Count; i++)
        {
            itemTransitionSprites[i] = inventoryItemImages[i].sprite;
        }

        //resolve target positions
        if (in_player == -1) //swapping
        {
            for (int i = 0; i < trackedGrid.nodes.Count; i++)
            {
                if (in_parameter)//isAltSwap
                {
                    itemTargetPositions[i] = itemRestingPositions[trackedGrid.nodes[i].linkB];
                }
                else
                {
                    itemTargetPositions[i] = itemRestingPositions[trackedGrid.nodes[i].linkA];
                }
            }

            dropedItemIndex = -1;
            
        }

    }

    public void CalledAfterSwap(int in_player, bool in_parameter)
    {
        if (in_player != -1)
        {
            int playerNodeIndex = -2;//there will be a OOB Index exception if no playernode is found

            for (int i = 0; i < trackedGrid.nodes.Count; i++)
            {
                if (in_player == 1 &&
                    trackedGrid.nodes[i].property == InventoryNode.NodeProperty.np_Player1)
                {
                    playerNodeIndex = i;
                    break;
                }//redundant but readability
                else if (in_player == 2 &&
                         trackedGrid.nodes[i].property == InventoryNode.NodeProperty.np_Player2)
                {
                    playerNodeIndex = i;
                    break;
                }
            }

            if (in_player == 1)
            {
                itemTargetPositions[playerNodeIndex] = player1Portrait.transform.position;
            }
            else
            {
                itemTargetPositions[playerNodeIndex] = player2Portrait.transform.position;
            }

            if (in_parameter) //isDrop
            {
                dropedItemIndex = playerNodeIndex;
            }
            else
            {
                dropedItemIndex = -1;
            }
        }
    }
}

