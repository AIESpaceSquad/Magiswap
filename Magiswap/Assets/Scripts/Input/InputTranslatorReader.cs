﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputTranslatorReader : MonoBehaviour {

    [SerializeField]
    InputTranslator readTranslator;
    [SerializeField]
    Text outputBox;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        string moveState = StateToString(readTranslator.MovementCode);
        string actState = StateToString(readTranslator.ActionCode);

        outputBox.text = "ms: " + moveState + " age: " + (int)readTranslator.moveStateAge + "| as: " + actState + " age: " + (int)readTranslator.actionStateAge;
	}


    string StateToString(InputTranslator.StateCode in_stateCode)
    {
        switch (in_stateCode)
        {
            case InputTranslator.StateCode.state_idle:
                return "Idle";
            case InputTranslator.StateCode.state_blocked:
                return "Blocked";
            case InputTranslator.StateCode.state_mov_right:
                return "Move Right";
            case InputTranslator.StateCode.state_mov_left:
                return "Move Left";
            case InputTranslator.StateCode.state_mov_up:
                return "Move Up";
            case InputTranslator.StateCode.state_mov_down:
                return "Move Down";
            case InputTranslator.StateCode.state_mov_turn:
                return "Move Turn";
            case InputTranslator.StateCode.state_act_jump:
                return "Jump";
            case InputTranslator.StateCode.state_act_swap_pri:
                return "Swap";
            case InputTranslator.StateCode.state_act_swap_alt:
                return "Alt Swap";
            case InputTranslator.StateCode.state_act_activate:
                return "Activate";
            default:
                return "error";
        }
    }


}
