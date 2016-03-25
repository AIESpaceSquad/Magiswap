using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{

    public GameObject InputManager;
    public Sprite JoystickSprite;
    string[] controllerNames;
    // Use this for initialization
	void Start ()
    {
        controllerNames = Input.GetJoystickNames();
        Debug.Log(controllerNames);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
