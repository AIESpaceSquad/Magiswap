using UnityEngine;
using System.Collections;

public class ActivatorActivator : Activateable
{
    [SerializeField]
    Activateable target;
    [SerializeField]
    bool isToggle = false;
    bool isOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    protected override void OnActivateImmediate(Item in_itemUsed)
    {
        if (isOn && isToggle)
        {
        
            target.ForceReset();
            isOn = false;
        }
        else
        {
            target.ForceActivate(in_itemUsed);
            isOn = true;
        }
    }
}
