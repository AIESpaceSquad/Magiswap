using UnityEngine;
using System.Collections;

public class multiDirElevator : Activateable {

    [SerializeField]
    Transform topLocation0;
    [SerializeField]
    Transform topLocation1;
    [SerializeField]
    Transform topLocation2;
    [SerializeField]
    Transform botLocation1;
    [SerializeField]
    Transform botLocation2;

    [SerializeField]
    float moveTime = 1.0f;

    float remainingMoveTime = 0.0f;

    Vector3[,] StopLocations;
    int xPos = 0;
    int yPos = 0;

    int xTarget = 0;
    int yTarget = 0;

    // Use this for initialization
    void Start () {
        StopLocations = new Vector3[3, 2];

        StopLocations[0, 0] = transform.position;
        StopLocations[1, 0] = botLocation1.position;
        StopLocations[2, 0] = botLocation2.position;

        StopLocations[0, 1] = topLocation0.position;
        StopLocations[1, 1] = topLocation1.position;
        StopLocations[2, 1] = topLocation2.position;

    }
	
	// Update is called once per frame
	void Update () {

        if (remainingMoveTime > 0.0f)
        {
            Vector3 source = StopLocations[xPos, yPos];
            Vector3 goal = StopLocations[xTarget, yTarget];
            //source and goal are backwards because the timer is a countown
            transform.position = Vector3.Lerp(goal, source, Mathf.Clamp(remainingMoveTime, 0, moveTime) / moveTime);

            remainingMoveTime -= Time.deltaTime;
            if (remainingMoveTime < 0.0f)
            {
                isActive = false;
                xPos = xTarget;
                yPos = yTarget;
            }
        }
	
	}

    protected override void Reset()
    {
        if (isActive == false)
        {
            xTarget = xPos; //redundant?
            yTarget = yPos + 1;
            if (yTarget > 1)
            {
                yTarget = 0;
            }
            remainingMoveTime = moveTime;

            isActive = true;
        }
    }

    protected override void OnActivateImmediate(Item in_itemUsed)
    {
        xTarget = xPos + 1;
        yTarget = yPos; //redundant?
        if (xTarget > 2)
        {
            xTarget = 0;
        }
        remainingMoveTime = moveTime;

        isActive = true;

        //isActive is automatically set
    }
}
