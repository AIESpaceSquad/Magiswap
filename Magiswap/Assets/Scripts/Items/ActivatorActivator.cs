using UnityEngine;
using System.Collections;

public class ActivatorActivator : Activateable
{
    [SerializeField]
    Activateable target;
    [SerializeField]
    activeMode activateMode = activeMode.am_activateOnly;
    [SerializeField]
    Animator targetAnimator;
    bool isOn = false;

    public enum activeMode
    {
        am_toggle,
        am_activateOnly,
        am_resetOnly,
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target.isActive)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
	}

    protected override void OnActivateImmediate(Item in_itemUsed)
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetTrigger("AcitvatorTrigger");
        }
        switch (activateMode) {
            case activeMode.am_toggle:
                if (isOn)
                {

                    target.ForceReset();
                    isOn = false;
                }
                else
                {
                    target.ForceActivate(in_itemUsed);
                    isOn = true;
                }
                break;
            case activeMode.am_activateOnly:
                target.ForceActivate();
                break;
            case activeMode.am_resetOnly:
                target.ForceReset();
                break;
            default:
                Debug.Log("activatorActivator has an invalid mode");
                break;
        }
    }
}
