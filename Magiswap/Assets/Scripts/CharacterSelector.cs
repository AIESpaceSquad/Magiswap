using UnityEngine;
using System.Collections;

public class CharacterSelector : MonoBehaviour
{
    public ControllerManager cManager;
    public GameObject joystick;
    GameObject jStick1;
    GameObject jStick2;
	// Use this for initialization
	void Start ()
    {
        Debug.Log("Number of controllers in manager: " + cManager.controllerList.Count);
        for (int i = 0; i < cManager.controllerList.Count; i++)
        {
            jStick1 = Instantiate(joystick, transform.position, Quaternion.identity) as GameObject;
            jStick1.transform.localScale *= 30;
            jStick1.transform.parent = transform;
            //Debug.Log("Created a jstick obj");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(cManager.controllerList[0].controllerName + "_rStickX");
        if(Input.GetAxis(cManager.controllerList[0].controllerName + "rStickX") >= 1 ||
           Input.GetAxis(cManager.controllerList[0].controllerName + "rStickX") <= 1)
        {
            jStick1.transform.position += new Vector3(Input.GetAxis(cManager.controllerList[0].controllerName + "rStickX"),0 , 0);
            
        }

	}
}
