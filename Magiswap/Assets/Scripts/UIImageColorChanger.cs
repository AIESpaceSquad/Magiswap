using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIImageColorChanger : MonoBehaviour {

    Image myUIImage;

	// Use this for initialization
	void Start () {
        myUIImage = GetComponent<Image>();

        if (myUIImage == null)
        {
            Debug.Log("UI Inmage not found");
        }
	}
	
    public void ChangeImageColor(ColorManager.CollisionColor in_NewColor)
    {
        myUIImage.color = ColorManager.GetActualColor(in_NewColor);
    }

}
