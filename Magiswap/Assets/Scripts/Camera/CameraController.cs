using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField]
    GameObject mainTarget;

    [SerializeField]
    float walkingBoundry = 2;
    [SerializeField]
    float transitionBoundry = 4;

    Rigidbody2D playerRigidbody;
    listenerCharacter playerCharacter;

    [SerializeField]
    float xTransitionLength = 2.5f;
    [SerializeField]
    float xTransitionTime = 0.0f;

    bool walkingBoundryIsOnRight = true;

	// Use this for initialization
	void Start () {
        playerRigidbody = mainTarget.GetComponent<Rigidbody2D>();
        playerCharacter = mainTarget.GetComponent<listenerCharacter>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float xMovement = 0.0f;
        float xDiff = mainTarget.transform.position.x - transform.position.x;
        if (walkingBoundryIsOnRight)
        {
            if (xDiff > walkingBoundry)
            {
                xMovement = xDiff - walkingBoundry;
            }
            else if (xDiff < -transitionBoundry)
            {
                xTransitionTime = xTransitionLength;
                walkingBoundryIsOnRight = false;
            }
        }
        else
        {
            if (xDiff < -walkingBoundry)
            {
                xMovement = xDiff + walkingBoundry;
            }
            else if (xDiff > transitionBoundry)
            {
                xTransitionTime = xTransitionLength;
                walkingBoundryIsOnRight = true;
            }
        }

        if (xTransitionTime > 0.0f)
        {
            xMovement *= 1.0f - (xTransitionTime / xTransitionLength);
        }

        transform.Translate(xMovement, 0, 0);
        
    }

    void Update()
    {
        if (xTransitionTime > 0.0f)
        {
            xTransitionTime -= Time.deltaTime;
        }
    } 

}
