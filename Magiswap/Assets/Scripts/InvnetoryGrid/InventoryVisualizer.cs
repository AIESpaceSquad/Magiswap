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
    float imageSize;

    [SerializeField]
    InventoryGrid displayedGrid;

    List<Text> updatedTexts;

    // Use this for initialization
    void Start () {
        updatedTexts = new List<Text>();

        GameObject imageTemplate = new GameObject();
        imageTemplate.AddComponent<CanvasRenderer>();
        imageTemplate.AddComponent<Image>();

        GameObject textTemplate = new GameObject();
        textTemplate.AddComponent<CanvasRenderer>();
        textTemplate.AddComponent<Text>();

        for (int i = 0; i < displayedGrid.nodes.Count; i++) {
            GameObject newObject =  GameObject.Instantiate(imageTemplate);
            GameObject newText = GameObject.Instantiate(textTemplate);

            switch (displayedGrid.nodes[i].property)
            {
                case InventoryGrid.NodeProperty.np_Player1:
                    newObject.GetComponent<Image>().sprite = player1Image;
                    break;
                case InventoryGrid.NodeProperty.np_Player2:
                    newObject.GetComponent<Image>().sprite = player2Image;
                    break;
                case InventoryGrid.NodeProperty.np_Symbol1:
                    newObject.GetComponent<Image>().sprite = Symbol1Image;
                    break;
                case InventoryGrid.NodeProperty.np_Symbol2:
                    newObject.GetComponent<Image>().sprite = Symbol2Image;
                    break;
                case InventoryGrid.NodeProperty.np_Symbol3:
                    newObject.GetComponent<Image>().sprite = Symbol3Image;
                    break;
                default:
                    newObject.GetComponent<Image>().sprite = defaultImage;
                    break;
            }


            newText.GetComponent<Text>().text = displayedGrid.nodes[i].item.ToString();
            newText.GetComponent<Text>().font = ItemFont;
            updatedTexts.Add(newText.GetComponent<Text>());

            newObject.transform.SetParent(transform, false);
            newText.transform.SetParent(transform, false);

            newObject.transform.position = new Vector3((imageSize * displayedGrid.nodes[i].offsetX) + transform.position.x, (imageSize * displayedGrid.nodes[i].offsetY) + transform.position.y, 1);
            newText.transform.position = new Vector3(((imageSize * displayedGrid.nodes[i].offsetX) + transform.position.x) / 2, ((imageSize * displayedGrid.nodes[i].offsetY) + transform.position.y) / 2, 0);
        }

        GameObject.Destroy(imageTemplate);
        GameObject.Destroy(textTemplate);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            displayedGrid.Swap(false);
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            displayedGrid.Swap(true);
        }

        for (int i = 0; i < displayedGrid.nodes.Count; i++)
        {
            updatedTexts[i].text = displayedGrid.nodes[i].item.ToString();
        }

	}
}
