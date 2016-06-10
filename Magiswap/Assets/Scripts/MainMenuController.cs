using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    Image buttonImagePlay;
    [SerializeField]
    Image buttonImageExit;

    [SerializeField]
    float inputCooldownlength = 0.5f;
    float remainingInputCooldown = 0.0f;

    const int indexCount = 2;
    int currentIndex;


	// Use this for initialization
	void Start () {
        ChangeIndex(0);
	}
	
	// Update is called once per frame
	void Update () {

        if (remainingInputCooldown > 0.0f)
        {
            remainingInputCooldown -= Time.deltaTime;
        }
        else
        {
            if (ControllerHandler.MenuControllerGetYAxis() > 0)
            {
                remainingInputCooldown = inputCooldownlength;
                ChangeIndex(currentIndex + 1);
            }
            else if (ControllerHandler.MenuControllerGetYAxis() < 0)
            {
                remainingInputCooldown = inputCooldownlength;
                ChangeIndex(currentIndex - 1);
            }
            else if (ControllerHandler.MenuControllerGetActivateButton())
            {
                remainingInputCooldown = inputCooldownlength;
                ActivateIndex(currentIndex);
            }
        }
    }

    void ChangeIndex(int in_newIndex)
    {
        if (in_newIndex >= indexCount)
        {
            currentIndex = 0;
        }
        else if (in_newIndex < 0)
        {
            currentIndex = indexCount - 1;
        }
        else
        {
            currentIndex = in_newIndex;
        }

        buttonImagePlay.enabled = false;
        buttonImageExit.enabled = false;

        switch (currentIndex)
        {
            case 0:
                buttonImagePlay.enabled = true;
                break;
            case 1:
                buttonImageExit.enabled = true;
                break;
        }

    }

    void ActivateIndex(int in_activeIndex)
    {
        switch (in_activeIndex)
        {
            case 0:
                //go to play scene
                break;
            case 1:
                Application.Quit();
                break;
            default:
                Debug.Log("Main Menu Activated Index oob");
                break;
        }
    }
}
