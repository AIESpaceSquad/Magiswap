using UnityEngine;
using System.Collections;

public class ColorTester : MonoBehaviour {

    [SerializeField]
    GameObject subject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if( Input.GetKeyDown(KeyCode.Q))
        {
            ColorManager.ChangeColor(subject, ColorManager.CollisionColor.cc_StaticWhite);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ColorManager.ChangeColor(subject, ColorManager.CollisionColor.cc_ActiveWhite);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ColorManager.ChangeColor(subject, ColorManager.CollisionColor.cc_Red);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ColorManager.ChangeColor(subject, ColorManager.CollisionColor.cc_Blue);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            ColorManager.ChangeColor(subject, ColorManager.CollisionColor.cc_yellow);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ColorManager.ChangeColor(subject, ColorManager.CollisionColor.cc_green);
        }
    }
}
