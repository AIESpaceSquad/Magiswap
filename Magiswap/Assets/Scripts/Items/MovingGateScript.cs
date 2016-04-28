using UnityEngine;
using System.Collections;

public class MovingGateScript : Activateable {

    [SerializeField]
    Transform targetPos;
    [SerializeField]
    float TransitionLength = 2.0f;
    float TransitionTime = 0.0f;

    Vector2 targetActual;
    Vector2 startActual;

	// Use this for initialization
	void Start () {
        targetActual = targetPos.position;
        startActual = transform.position;
        TransitionTime = TransitionLength;
	}
	
	// Update is called once per frame
	void Update () {

        if (isActive)
        {
            if (TransitionTime > 0.0f)
            {
                TransitionTime -= Time.deltaTime;
            }
        }

        Vector3 newPos = Vector2.Lerp(startActual, targetActual, 1.0f - TransitionTime / TransitionLength);

        newPos.z = transform.position.z;

        transform.position = newPos;
	    
	}
}
