using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryVisualizer : MonoBehaviour {

    [SerializeField]
    Sprite defaultImage;
    [SerializeField]
    Sprite player1Image;
    [SerializeField]
    Sprite player2Image;
    [SerializeField]
    Sprite Symbol1Image;
    [SerializeField]
    Sprite Symbol2Image;
    [SerializeField]
    Sprite Symbol3Image;
    [SerializeField]
    Font ItemFont;

    [SerializeField]
    float imageScale = 1;
    float HalfImageSize = 75;

    [SerializeField]
    float backTileOpacity = 0.8f;
    [SerializeField]
    float frontTileOpacity = 1.0f;

    [SerializeField]
    InventoryGrid displayedGrid;

    List<Image> updatedImages;
    List<Transform> movedImages;
    List<Image> backgroundImages;

    int numGridSlots;
    Vector3[] itemRestingPositions;

    bool MovementActive;
    Sprite[] tempSprites;
    Vector3[] itemEndPosition;

    // Use this for initialization
    void Start () {

        numGridSlots = 0;
        
        updatedImages = new List<Image>();
        movedImages = new List<Transform>();
        backgroundImages = new List<Image>();

        GameObject imageTemplate = new GameObject();
        imageTemplate.AddComponent<CanvasRenderer>();
        imageTemplate.AddComponent<Image>();

        GameObject textTemplate = new GameObject();
        textTemplate.AddComponent<CanvasRenderer>();
        textTemplate.AddComponent<Image>();

        numGridSlots = displayedGrid.nodes.Count;
        itemRestingPositions = new Vector3[numGridSlots];
        tempSprites = new Sprite[numGridSlots];
        itemEndPosition = new Vector3[numGridSlots];

        for (int i = 0; i < displayedGrid.nodes.Count; i++) {
            GameObject newObject =  GameObject.Instantiate(imageTemplate);
            GameObject newText = GameObject.Instantiate(textTemplate);

            switch (displayedGrid.nodes[i].property)
            {
                case InventoryNode.NodeProperty.np_Player1:
                    newObject.GetComponent<Image>().sprite = player1Image;
                    break;
                case InventoryNode.NodeProperty.np_Player2:
                    newObject.GetComponent<Image>().sprite = player2Image;
                    break;
                case InventoryNode.NodeProperty.np_Symbol1:
                    newObject.GetComponent<Image>().sprite = Symbol1Image;
                    break;
                case InventoryNode.NodeProperty.np_Symbol2:
                    newObject.GetComponent<Image>().sprite = Symbol2Image;
                    break;
                case InventoryNode.NodeProperty.np_Symbol3:
                    newObject.GetComponent<Image>().sprite = Symbol3Image;
                    break;
                default:
                    newObject.GetComponent<Image>().sprite = defaultImage;
                    break;
            }

            newObject.GetComponent<Image>().color = new Color(1, 1, 1, backTileOpacity);

            //newText.GetComponent<Text>().text = displayedGrid.nodes[i].item.ToString();
            //newText.GetComponent<Text>().font = ItemFont;
            if (displayedGrid.nodes[i].item != null)
            {
                newText.GetComponent<Image>().sprite = displayedGrid.nodes[i].item.GetComponent<Item>().UISprite;
            }
            else
            {
                newText.GetComponent<Image>().sprite = defaultImage;
            }
            backgroundImages.Add(newObject.GetComponent<Image>());
            updatedImages.Add(newText.GetComponent<Image>());
            movedImages.Add(newText.transform);

            newObject.transform.SetParent(transform, false);
            newText.transform.SetParent(transform, false);

            newObject.transform.position = new Vector3((HalfImageSize * displayedGrid.nodes[i].offsetX) + transform.position.x, (HalfImageSize * displayedGrid.nodes[i].offsetY) + transform.position.y, 1);
            newText.transform.position = new Vector3(((HalfImageSize * displayedGrid.nodes[i].offsetX) + transform.position.x), ((HalfImageSize * displayedGrid.nodes[i].offsetY) + transform.position.y), 0);
            itemRestingPositions[i] = newText.transform.position;
        }

        GameObject.Destroy(imageTemplate);
        GameObject.Destroy(textTemplate);

        gameObject.transform.localScale = new Vector3(imageScale, imageScale, imageScale);
        //firstPlayerNode = displayedGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1);
    }

    // Update is called once per frame
    void Update() {

        //float currentPrecentage = InventoryControl.RemainingCooldown / InventoryControl.SwapCooldown; //lerps will be backwards as this is the remaining time
        //if (currentPrecentage <= 0.0f)
        //{
        //    MovementActive = false;
        //}

        for (int i = 0; i < updatedImages.Count; i++)
        {
            if (displayedGrid.nodes[i].item != null)
            {
                //if (MovementActive)
                //{
                //    movedImages[i].position = Vector3.Lerp(itemEndPosition[i], itemRestingPositions[i], currentPrecentage);
                //    updatedImages[i].sprite = tempSprites[i];
                //}
                //else
                //{
                    //movedImages[i].position = itemRestingPositions[i];
                    updatedImages[i].sprite = displayedGrid.nodes[i].item.GetComponent<Item>().UISprite;
                //}
                updatedImages[i].color = new Color(1, 1, 1, frontTileOpacity);
                Color bgColor = ColorManager.GetActualColor(displayedGrid.nodes[i].item);
                backgroundImages[i].color = new Color(bgColor.r, bgColor.g, bgColor.b, backTileOpacity);
            }
            else
            {
                updatedImages[i].color = new Color(0, 0, 0, 0);
                backgroundImages[i].color = new Color(1, 1, 1, backTileOpacity);
                //updatedImages[i].sprite = defaultImage;
            }
        }

    }

    public void callBeforeChange(int in_player, bool in_argument)
    {
        for (int i = 0; i < tempSprites.Length; i++)
        {
            tempSprites[i] = updatedImages[i].sprite;
        }

        if (in_player == -1)//swapping
        {
            if (in_argument)//use alt swap
            {
                for (int i = 0; i < itemEndPosition.Length; i++)
                {
                    itemEndPosition[i] = itemRestingPositions[displayedGrid.nodes[i].linkB]; //moves to the resting position of it's link
                }
            }
            else //don't use alt swap
            {
                for (int i = 0; i < itemEndPosition.Length; i++)
                {
                    itemEndPosition[i] = itemRestingPositions[displayedGrid.nodes[i].linkA];
                }
            }
        }
        else // droping/ picking up
        {
            for (int i = 0; i < itemRestingPositions.Length; i++)
            {
                itemEndPosition[i] = itemRestingPositions[i];
            }
        }
    }

    public void callAfterChange(int in_player, bool in_argument)
    {
        if (in_player != 0)
        {

            int playerNodeIndex = 0;
            InventoryNode playerNode = null;
            InventoryNode.NodeProperty targetProperty;

            if (in_player == 1) {
                targetProperty = InventoryNode.NodeProperty.np_Player1;
            }
            else if (in_player == 2)
            {
                targetProperty = InventoryNode.NodeProperty.np_Player2;
            }
            else
            {
                Debug.Log("player given is invalid");
                return;
            }

            while (playerNodeIndex < displayedGrid.nodes.Count)
            {
                if (displayedGrid.nodes[playerNodeIndex].property == targetProperty)
                {
                    playerNode = displayedGrid.nodes[playerNodeIndex];
                    break;
                }
                playerNodeIndex++;
            }

            if (in_argument) //isDrop
            {
                if (in_player == 1)
                {
                    itemEndPosition[playerNodeIndex] = itemEndPosition[playerNodeIndex] + new Vector3(0, HalfImageSize * 2, 0);
                }
                else
                {
                    itemEndPosition[playerNodeIndex] = itemEndPosition[playerNodeIndex] + new Vector3(0, -HalfImageSize * 2, 0);
                }
            }
            else
            {
                tempSprites[playerNodeIndex] = playerNode.item.GetComponent<Item>().UISprite;
                itemEndPosition[playerNodeIndex] = itemEndPosition[playerNodeIndex] + new Vector3(HalfImageSize, 0, 0);
                
            }
        }

        MovementActive = true;
    }
}
