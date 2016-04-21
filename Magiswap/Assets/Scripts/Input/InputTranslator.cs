using UnityEngine;
using System.Collections;

public class InputTranslator {

    public enum StateCode
    {
        state_idle,
        state_blocked,
        state_mov_right,
        state_mov_left,
        state_mov_up,
        state_mov_down,
        state_mov_turn,
        state_act_jump,
        state_act_swap_pri,
        state_act_swap_alt,
        state_act_activate,
    }

    private StateCode movementCode;
    [HideInInspector]
    public float moveStateAge
    {
        private set;
        get;
    }
    [HideInInspector]
    public StateCode MovementCode
    {
        private set
        {
            if (value != movementCode)
            {
                moveStateAge = 0.0f;
                movementCode = value;
            }
        }
        get
        {
            return movementCode;
        }
    }

    private StateCode actionCode;
    [HideInInspector]
    public float actionStateAge
    {
        private set;
        get;
    }
    [HideInInspector]
    public StateCode ActionCode
    {
        private set
        {
            if (value != actionCode)
            {
                actionStateAge = 0.0f;
                actionCode = value;
            }
        }
        get
        {
            return actionCode;
        }
    }

    [SerializeField]
    private float actionWaitLimit = 0.5f;
    public string controllerName;

	// Use this for initialization
	public void Start () {
        MovementCode = StateCode.state_idle;
        ActionCode = StateCode.state_idle;
	}
	
	// Update is called once per frame
	public void Update () {
        moveStateAge += Time.deltaTime;
        actionStateAge += Time.deltaTime;

        if (movementCode != StateCode.state_idle)
        {
            if (moveStateAge > actionWaitLimit)
            {
                MovementCode = StateCode.state_idle;
            }
        }

        if (ActionCode != StateCode.state_idle)
        {
            if (actionStateAge > actionWaitLimit)
            {
                ClearAction();
            }
        }

        float movementX = Input.GetAxis(controllerName + "_moveX");

        if (movementX > 0.2f)
        {
            MovementCode = StateCode.state_mov_right;
        }
        else if (movementX < -0.2f)
        {
            MovementCode = StateCode.state_mov_left;
        } 
        else
        {
            MovementCode = StateCode.state_idle;
        }

        if (Input.GetButtonDown(controllerName + "_jump"))
        {
            ActionCode = StateCode.state_act_jump;
        }
        else if (Input.GetButtonDown(controllerName + "_activate"))
        {
            ActionCode = StateCode.state_act_activate;
        }
        else if (Input.GetButtonDown(controllerName + "_swap"))
        {
            ActionCode = StateCode.state_act_swap_pri;
        }
        else if (Input.GetButtonDown(controllerName + "_swapAlt"))
        {
            ActionCode = StateCode.state_act_swap_alt;
        }

	}

    public void ClearAction()
    {
        ActionCode = StateCode.state_idle;
    }

}
