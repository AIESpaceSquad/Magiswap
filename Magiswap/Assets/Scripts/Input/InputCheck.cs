using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputCheck : MonoBehaviour {

    [SerializeField]
    string controller;

    [SerializeField]
    Text jumpText;
    [SerializeField]
    Text activateText;
    [SerializeField]
    Text swapText;
    [SerializeField]
    Text altSwapText;
    [SerializeField]
    Text backText;
    [SerializeField]
    Text startText;
    [SerializeField]
    Text moveXText;
    [SerializeField]
    Text moveYText;
    [SerializeField]
    Text rStickXText;
    [SerializeField]
    Text rStickYText;
    [SerializeField]
    Text TriggersText;
    [SerializeField]
    Text dPadXText;
    [SerializeField]
    Text dPadYText;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        jumpText.text     = Input.GetButton(controller + "_jump"    ).ToString();
        activateText.text = Input.GetButton(controller + "_activate").ToString();
        swapText.text     = Input.GetButton(controller + "_swap"    ).ToString();
        altSwapText.text  = Input.GetButton(controller + "_swapAlt" ).ToString();
        backText.text     = Input.GetButton(controller + "_back"    ).ToString();
        startText.text    = Input.GetButton(controller + "_start"   ).ToString();
        moveXText.text    = Input.GetAxis(  controller + "_moveX"   ).ToString();
        moveYText.text    = Input.GetAxis(  controller + "_moveY"   ).ToString();

        if (controller.CompareTo("kb0") == 0)
        {
            rStickXText.text  = "n/a";
            rStickYText.text  = "n/a";
            TriggersText.text = "n/a";
            dPadXText.text    = "n/a";
            dPadYText.text    = "n/a";
        }
        else
        {
            rStickXText.text  = Input.GetAxis(controller + "_rStickX" ).ToString();
            rStickYText.text  = Input.GetAxis(controller + "_rStickY" ).ToString();
            TriggersText.text = Input.GetAxis(controller + "_triggers").ToString();
            dPadXText.text    = Input.GetAxis(controller + "_dPadX"   ).ToString();
            dPadYText.text    = Input.GetAxis(controller + "_dPadY"   ).ToString();
        }
    }
}
