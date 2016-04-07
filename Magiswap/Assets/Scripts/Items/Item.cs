using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    //when items are picked up they sould be completely disabled
    //when they are droped they should be enabled after they are moved

    //considering making a scriptable object for this

    public int activatorKey = 0;
    public Sprite UISprite = null;
    //float floatingDistance = 0.2f;//?

	// Use this for initialization
	void Start () {
	    //setup so we can glow the right color
	}
	
	// Update is called once per frame
	void Update () {
	    //stuff to keep us glowing the right color
        //also stuff so we float off the ground?
	}
}
