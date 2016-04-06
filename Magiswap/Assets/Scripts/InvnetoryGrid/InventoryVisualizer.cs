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
    float HalfImageSize = 50;

    [SerializeField]
    float backTileOpacity = 0.8f;
    [SerializeField]
    float frontTileOpacity = 1.0f;

    [SerializeField]
    InventoryGrid displayedGrid;

    //[SerializeField]
    //GameObject TestItem0;
    //[SerializeField]
    //GameObject TestItem1;
    //[SerializeField]
    //GameObject TestItem2;
    //[SerializeField]
    //GameObject TestItem3;

    //InventoryNode firstPlayerNode;

    List<Image> updatedImages;

    // Use this for initialization
    void Start () {
        
        updatedImages = new List<Image>();

        GameObject imageTemplate = new GameObject();
        imageTemplate.AddComponent<CanvasRenderer>();
        imageTemplate.AddComponent<Image>();

        GameObject textTemplate = new GameObject();
        textTemplate.AddComponent<CanvasRenderer>();
        textTemplate.AddComponent<Image>();

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
            updatedImages.Add(newText.GetComponent<Image>());

            newObject.transform.SetParent(transform, false);
            newText.transform.SetParent(transform, false);

            newObject.transform.position = new Vector3((HalfImageSize * displayedGrid.nodes[i].offsetX) + transform.position.x, (HalfImageSize * displayedGrid.nodes[i].offsetY) + transform.position.y, 1);
            newText.transform.position = new Vector3(((HalfImageSize * displayedGrid.nodes[i].offsetX) + transform.position.x), ((HalfImageSize * displayedGrid.nodes[i].offsetY) + transform.position.y), 0);
        }

        GameObject.Destroy(imageTemplate);
        GameObject.Destroy(textTemplate);

        gameObject.transform.localScale = new Vector3(imageScale, imageScale, imageScale);
        //firstPlayerNode = displayedGrid.GetSpecialSlot(InventoryNode.NodeProperty.np_Player1);
    }

    // Update is called once per frame
    void Update() {

        //if (Input.GetKeyDown(KeyCode.Comma))
        //{
        //    displayedGrid.Swap(false);
        //}
        //if (Input.GetKeyDown(KeyCode.Period))
        //{
        //    displayedGrid.Swap(true);
        //}
        //if (!firstPlayerNode.item)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        if (TestItem0.activeSelf == true)
        //        {
        //            firstPlayerNode.item = TestItem0;
        //            TestItem0.SetActive(false);
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha2))
        //    {
        //        if (TestItem1.activeSelf == true)
        //        {
        //            firstPlayerNode.item = TestItem1;
        //            TestItem1.SetActive(false);
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha3))
        //    {
        //        if (TestItem2.activeSelf == true)
        //        {
        //            firstPlayerNode.item = TestItem2;
        //            TestItem2.SetActive(false);
        //        }
        //    }
        //    if (Input.GetKeyDown(KeyCode.Alpha4))
        //    {
        //        if (TestItem3.activeSelf == true)
        //        {
        //            firstPlayerNode.item = TestItem3;
        //            TestItem3.SetActive(false);
        //        }
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha0))
        //    {
        //        GameObject releasedItem = firstPlayerNode.item;
        //        firstPlayerNode.item = null;
        //        releasedItem.SetActive(true);
        //    }
        //}

        for (int i = 0; i < updatedImages.Count; i++)
        {
            if (displayedGrid.nodes[i].item != null)
            {
                updatedImages[i].color = new Color(1, 1, 1, frontTileOpacity);
                updatedImages[i].sprite = displayedGrid.nodes[i].item.GetComponent<Item>().UISprite;
            }
            else
            {
                updatedImages[i].color = new Color(0, 0, 0, 0);
                //updatedImages[i].sprite = defaultImage;
            }
        }

    }
}
