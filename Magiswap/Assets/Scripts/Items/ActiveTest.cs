using UnityEngine;
using System.Collections;

public class ActiveTest : MonoBehaviour {

    [SerializeField]
    Activateable activeatedItem;
    [SerializeField]
    Item usedItem;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            activeatedItem.AttemptActivate(usedItem);
        }
	}
}
