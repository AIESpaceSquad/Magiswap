using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {

    MenuTemplate myTemplate;

    GameObject greaterTitle;
    GameObject title;
    GameObject[] items;

    int currentMenu;
    int currentItem;

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
        }
    }

	// Use this for initialization
	void Start()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    
    int GetMenu(string in_name)
    {
        return 0;
    }
}
