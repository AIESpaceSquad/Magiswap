using UnityEngine;
using System.Collections;

public class ElevatorScript : Activateable
{
    [SerializeField]
    Transform goal;

    Vector3 start;
    Vector3 end;

    [SerializeField]
    float travelTime = 3.0f;

    bool isAtStart = true;
    float remainingTravelTime = 0.0f;

	// Use this for initialization
	void Start () {
        end = goal.position;
        start = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
	    if (remainingTravelTime > 0.0f)
        {
            if (isAtStart)
            {
                //timer is a countdown so 1 is the start and 0 is the end, meanig we have to reverse start and end
                transform.position = Vector3.Lerp(end, start, Mathf.Clamp(remainingTravelTime, 0.0f, travelTime) / travelTime);
            }
            else
            {
                transform.position = Vector3.Lerp(start, end, Mathf.Clamp(remainingTravelTime, 0.0f, travelTime) / travelTime);
            }

            remainingTravelTime -= Time.deltaTime;

            if (remainingTravelTime <= 0.0f)
            {
                isAtStart = !isAtStart;
                Reset();
            }
        }
	}

    protected override void OnActivateImmediate(Item in_itemUsed)
    {
        remainingTravelTime = travelTime;
    }
}
