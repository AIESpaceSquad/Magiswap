using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControllerHandler : MonoBehaviour {

    private static string[] controllerNames = { "kb0", "gp1", "gp2" };

    private static InputTranslator[] controllerReaders;

    private static float genralReadDeadzone = 0.2f;

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
        //constant return to menu button
        if (IsAnyButton("_start"))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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

    public static void ActionFulfilled(int in_controller)
    {
        if (in_controller < controllerReaders.Length && in_controller >= 0)
        {
            controllerReaders[in_controller].ClearAction();
        }
        else
        {
            Debug.Log("provided index is out of bounds");
        }
    }

    public static float MenuControllerGetXAxis()
    {
        
        return GetAnyAxis("_moveX");
    }

    public static float MenuControllerGetYAxis()
    {
        return GetAnyAxis("_moveY");
    }

    public static bool MenuControllerGetActivateButton()
    {
        return IsAnyButton("_jump");
    }

    public static bool MenuControllerGetReturnButton()
    {
        return IsAnyButton("_activate");
    }

    public static bool AnyControllerGetEscapeButton()
    {
        return IsAnyButton("_start");
    }

    static bool IsAnyButton(string in_axis)
    {
        bool isAnyTrue = false;
        for (int i = 0; i < controllerNames.Length; i++)
        {
            if (Input.GetButton(controllerNames[i] + in_axis))
            {
                isAnyTrue = true;
                break;
            }
        }

        return isAnyTrue;
    }

    static float GetAnyAxis(string in_axis)
    {
        float outputAxis = Input.GetAxisRaw(controllerNames[0] + in_axis);

        if (Mathf.Abs(outputAxis) > genralReadDeadzone)
        {
            return outputAxis;
        }

        outputAxis = Input.GetAxisRaw(controllerNames[1] + in_axis);
        outputAxis += Input.GetAxisRaw(controllerNames[2] + in_axis);

        return outputAxis;
    }
}
