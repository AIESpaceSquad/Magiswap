using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {

    [SerializeField]
    MenuTemplate myTemplate;

    [SerializeField]
    bool dontWrapSelector = true;

    GameObject greaterTitle;
    GameObject title;
    GameObject[] items;
    GameObject selector;

    int currentMenu;
    int currentItem;

    float waitTimeDuratuon = 0.2f;
    float waitTimeRemaining = 0.0f;

    void Awake()
    {
        items = new GameObject[4];

        foreach (Transform child in transform)
        {
            if (child.name.CompareTo("MenuGreaterTitle") == 0)
            {
                greaterTitle = child.gameObject;
                continue;
            }
            if (child.name.CompareTo("MenuTitle") == 0)
            {
                title = child.gameObject;
                continue;
            }
            if (child.name.CompareTo("MenuTags") == 0)
            {
                foreach (Transform subChild in child.transform)
                {
                    if (subChild.name.CompareTo("MenuItem0") == 0)
                    {
                        items[0] = subChild.gameObject;
                        continue;
                    }
                    if (subChild.name.CompareTo("MenuItem1") == 0)
                    {
                        items[1] = subChild.gameObject;
                        continue;
                    }
                    if (subChild.name.CompareTo("MenuItem2") == 0)
                    {
                        items[2] = subChild.gameObject;
                        continue;
                    }
                    if (subChild.name.CompareTo("MenuItem3") == 0)
                    {
                        items[3] = subChild.gameObject;
                        continue;
                    }
                }
            }
            if (child.name.CompareTo("MenuSelection") == 0)
            {
                selector = child.gameObject;
            }
        }
    }

	// Use this for initialization
	void Start()
    {
        ChangeMenu(myTemplate.rootMenu);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (waitTimeRemaining > 0.0f)
        {
            waitTimeRemaining -= Time.deltaTime;
        }
        else
        {
            float axis = Input.GetAxis("gp1_moveY");
            if (axis > 0)
            {
                MoveSelection(1, dontWrapSelector);
                waitTimeRemaining = waitTimeDuratuon;
            }
            if (axis < 0)
            {
                MoveSelection(-1, dontWrapSelector);
                waitTimeRemaining = waitTimeDuratuon;
            }
        }

        if (Input.GetButtonDown("gp1_jump"))
        {
            switch (myTemplate.SubMenus[currentMenu].nodeItems[currentItem].itemActions)
            {
                case MenuNode.NodeItem.ItemAction.ia_ChangeNode:
                    ChangeMenu(myTemplate.SubMenus[currentMenu].nodeItems[currentItem].itemParams);
                    break;
                case MenuNode.NodeItem.ItemAction.ia_DoNothing:
                    //does nothing
                    break;
                default:
                    Debug.Log("invalid enum value in MenuHandler");
                    break;
            }
        }
        if (Input.GetButtonDown("gp1_activate"))
        {
            ChangeMenu(myTemplate.SubMenus[currentMenu].returnNode);
        }
    }

    void ChangeMenu(int in_menuIndex)
    {
        currentMenu = in_menuIndex;
        ResetSelection();
        if (!myTemplate.SubMenus[in_menuIndex].hideTitle)
        {
            title.SetActive(true);
            title.GetComponentInChildren<Text>().text = myTemplate.SubMenus[in_menuIndex].nodeName;
        }
        else
        {
            title.SetActive(false);
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (i < myTemplate.SubMenus[in_menuIndex].nodeItems.Length)
            {
                items[i].SetActive(true);
                items[i].GetComponentInChildren<Text>().text = myTemplate.SubMenus[in_menuIndex].nodeItems[i].itemText;
            }
            else
            {
                items[i].SetActive(false);
            }

        }
    }

    void MoveSelection(int in_direction, bool in_dontWrap = true)
    {
        currentItem += in_direction;

        int highestIndex = myTemplate.SubMenus[currentMenu].nodeItems.Length - 1;
        
        if (in_dontWrap)
        {
            currentItem = Mathf.Clamp(currentItem, 0, highestIndex);
        }
        else
        {
            if (currentItem > highestIndex)
            {
                currentItem = 0;
            }
            if (currentItem < 0)
            {
                currentItem = highestIndex;
            }
        }

        selector.transform.position = new Vector3(selector.transform.position.x, items[currentItem].transform.position.y, 0);
    }

    void ResetSelection()
    {
        currentItem = 0;
        selector.transform.position = new Vector3(selector.transform.position.x, items[0].transform.position.y, 0);
    }

    void ChangeMenu(string in_menuName)
    {
        ChangeMenu(GetMenu(in_menuName));
    }
    
    int GetMenu(string in_name)
    {
        for (int i = 0; i < myTemplate.SubMenus.Count; i++)
        {
            if (myTemplate.SubMenus[i].nodeName.CompareTo(in_name) == 0)
            {
                return i;
            }
        }

        Debug.Log("Could not find item named: " + in_name);
        return -1;
    }
}
