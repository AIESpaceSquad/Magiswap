using UnityEngine;
using System.Collections;

public class NodeListenerTest : MonoBehaviour {

    SpriteRenderer mySpriteRenderer = null;

	// Use this for initialization
	void Start () {
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (mySpriteRenderer == null)
        {
            Debug.Log("NodeLitsenerTest did not find a sprite renderer");
        }
	}

    public void ChangeMySpriteColor(ColorManager.CollisionColor in_newColor)
    {
        switch (in_newColor)
        {
            case ColorManager.CollisionColor.cc_ActiveWhite:
                mySpriteRenderer.color = Color.white;
                break;
            case ColorManager.CollisionColor.cc_StaticWhite:
                mySpriteRenderer.color = Color.white;
                break;
            case ColorManager.CollisionColor.cc_Red:
                mySpriteRenderer.color = Color.red;
                break;
            case ColorManager.CollisionColor.cc_green:
                mySpriteRenderer.color = Color.blue;
                break;
            case ColorManager.CollisionColor.cc_Blue:
                mySpriteRenderer.color = Color.blue;
                break;
            case ColorManager.CollisionColor.cc_yellow:
                mySpriteRenderer.color = Color.yellow;
                break;
            default:
                mySpriteRenderer.color = Color.magenta;
                break;
        }
    }
}
