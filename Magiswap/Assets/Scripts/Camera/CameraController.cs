using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField]
    GameObject mainTarget;

    Rigidbody2D playerRigidbody;
    listenerCharacter playerCharacter;

    //X move vars
    [SerializeField]
    float walkingBoundry = 2;
    [SerializeField]
    float transitionBoundry = 4;

    [SerializeField]
    float xTransitionLength = 2.5f;
    float xTransitionTime = 0.0f;

    bool walkingBoundryIsOnRight = true;

    //Y move vars
    [SerializeField]
    float yMaxBoundry = 3;
    [SerializeField]
    float yMinBoundry = -4;
    [SerializeField]
    float yLockOffset = -3;

    float yCurrentLock;

    [SerializeField]
    float yTransitionLength = 1.5f;
    float yTransitionTime = 0.0f;

    bool waitingForLanding = true;

    [SerializeField]
    bool useZLock = true;
    float zLock = 0.0f;

	// Use this for initialization
	void Start () {
        playerRigidbody = mainTarget.GetComponent<Rigidbody2D>();
        playerCharacter = mainTarget.GetComponent<listenerCharacter>();

        zLock = transform.position.z;
        yCurrentLock = mainTarget.transform.position.y;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //Xmovement
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

        //y movement
        float yMovement = 0.0f;

        if  (playerCharacter.IsGrounded)
        {
            if (waitingForLanding)
            {
                yTransitionTime = yTransitionLength;
                waitingForLanding = false;
            }
            yCurrentLock = playerCharacter.transform.position.y;
            
        }
        else
        {
            waitingForLanding = true;
        }

        float yLockDiff = yCurrentLock - transform.position.y;
        float yPlayerDiff = playerCharacter.transform.position.y - transform.position.y;

        if (!waitingForLanding)
        {
            yMovement = yLockDiff + yLockOffset;
        }
        else if (yPlayerDiff > yMaxBoundry)
        {
            yMovement = yPlayerDiff - yMaxBoundry;
        }
        else if (yPlayerDiff < yMinBoundry)
        {
            yMovement = yPlayerDiff - yMinBoundry;
        }

        if (yTransitionTime > 0.0f)
        {
            yMovement *= 1.0f - (yTransitionTime / yTransitionLength);
        }

        //apply movement
        transform.Translate(xMovement, yMovement, 0);

        if (useZLock)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLock);
        }
        
    }

    void OnDrawGizmos()
    {
        //yCurrentLock
        //Gizmos.DrawSphere(new Vector3(transform.position.x, yCurrentLock, 0), 1);
    }

    void Update()
    {
        if (xTransitionTime > 0.0f)
        {
            xTransitionTime -= Time.deltaTime;
        }

        if (yTransitionTime > 0.0f)
        {
            yTransitionTime -= Time.deltaTime;
        }
    } 

}
