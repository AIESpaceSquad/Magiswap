using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //grab mycolor and cash in ennum?
	}

    public enum CollisionColor
    {
        cc_StaticWhite = 8,
        cc_ActiveWhite = 9,
        cc_Red = 10,
        cc_Blue = 11,
        cc_yellow = 12,
        cc_green = 13
    }

    public static void ChangeColor(GameObject in_target, CollisionColor in_newLayer)
    {
        in_target.layer = (int)in_newLayer;
    }

    public static bool IsColor(GameObject in_subject, CollisionColor in_compareColor)
    {
        return (in_subject.layer == (int)in_compareColor);
    }

    public static bool SameColor(GameObject in_right, GameObject in_left)
    {
        return (in_left.layer == in_right.layer);
    }

}
