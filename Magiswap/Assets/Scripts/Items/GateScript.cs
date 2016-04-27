using UnityEngine;
using System.Collections;

public class GateScript : Activateable
{
    [SerializeField]
    Collider2D targetCollider;

    //target animator

    bool isComplete = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!isComplete)
        {
            if (isActive)
            {
                //wait for animations

                //when anim is done
                targetCollider.enabled = false;
                isComplete = true;
            }
        }
	}

    protected override void OnActivateImmediate(Item in_itemUsed)
    {
        //start anims
    }
}
