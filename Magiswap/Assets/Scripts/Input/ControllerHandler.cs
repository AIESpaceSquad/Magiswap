using UnityEngine;
using System.Collections;

public class ControllerHandler : MonoBehaviour {

    private static string[] controllerNames = { "kb0", "gp1", "gp2" };

    private static InputTranslator[] controllerReaders;

	// Use this for initialization
	void Start () {

        if (controllerReaders == null)
        {
            controllerReaders = new InputTranslator[controllerNames.Length];
            for (int i = 0; i < controllerNames.Length; i++)
            {
                controllerReaders[i] = new InputTranslator();
                controllerReaders[i].controllerName = controllerNames[i];
                controllerReaders[i].Start();
            }
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < controllerReaders.Length; i++)
        {
            controllerReaders[i].Update();
        }
	}

    public static InputTranslator.StateCode GetControllerAcionState(int in_controller)
    {
        if (in_controller < controllerReaders.Length && in_controller >= 0)
        {
            return controllerReaders[in_controller].ActionCode;
        }
        Debug.Log("provided index is out of bounds");
        return InputTranslator.StateCode.state_blocked;
    }

    public static InputTranslator.StateCode GetControllerMovementState(int in_controller)
    {
        if (in_controller < controllerReaders.Length && in_controller >= 0)
        {
            return controllerReaders[in_controller].MovementCode;
        }
        Debug.Log("provided index is out of bounds");
        return InputTranslator.StateCode.state_blocked;
    }

    public static float GetControllerActionStateAge(int in_controller)
    {
        if (in_controller < controllerReaders.Length && in_controller >= 0)
        {
            return controllerReaders[in_controller].actionStateAge;
        }
        Debug.Log("provided index is out of bounds");
        return Mathf.Infinity;
    }

    public static float GetControllerMovemnetStateAge(int in_controller)
    {
        if (in_controller < controllerReaders.Length && in_controller >= 0)
        {
            return controllerReaders[in_controller].moveStateAge;
        }
        Debug.Log("provided index is out of bounds");
        return Mathf.Infinity;
    }
}
