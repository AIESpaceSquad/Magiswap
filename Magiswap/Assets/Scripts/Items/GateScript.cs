using UnityEngine;
using System.Collections;

public class GateScript : Activateable
{
    [SerializeField]
    Collider2D targetCollider;
    [SerializeField]
    Animator targetAnimator;

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
                if ((targetAnimator != null && targetAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) ||
                   targetAnimator == null)
                {
                    targetCollider.enabled = false;
                    isComplete = true;
                }
            }
        }
	}

    protected override void OnActivateImmediate(Item in_itemUsed)
    {
        //start anims
        if (targetAnimator != null)
        {
            targetAnimator.SetBool("AcitvatorTrigger", true);
        }
    }
}
