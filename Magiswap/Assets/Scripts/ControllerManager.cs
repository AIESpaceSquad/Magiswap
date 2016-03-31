using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller
{
    public string controllerType;
    public string controllerName;
    public Controller(string tContollerType, int index)
    {
        controllerType = tContollerType;
        if(controllerType == "Controller (XBOX 360 For Windows)" ||
           controllerType == "Controller (XBOX One For Windows)")
        {
            controllerName = "gp" + (index + 1) + "_";
            Debug.Log(controllerName);
        }
    }
}


public class ControllerManager : MonoBehaviour
{
    public List<Controller> controllerList = new List<Controller>();
    public int numberOfControllers;
	// Use this for initialization
	void Awake ()
    {
        string[] _tControllerType = Input.GetJoystickNames();
       
        for (int i = 0; i < _tControllerType.Length; i++)
        {
            controllerList.Add(new Controller(_tControllerType[i], i));
            Debug.Log("Controllers Name: " + _tControllerType[i]);
            numberOfControllers++;
        }
        Debug.Log("Number of controllers in manager: " + controllerList.Count);
        //numberOfControllers = _tControllerType.Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
