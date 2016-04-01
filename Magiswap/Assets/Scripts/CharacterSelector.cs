using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class jStick
{
    public GameObject gObj;
    public int currentItem;
    public bool selected;
    public float spriteOffset;
    public float inputTime;
}

public class CharacterSelector : MonoBehaviour
{
    enum GAMESTATE
    {
        SINGLEPLAYER,
        LOCALMULTIPLAYER,
        ONLINEMULTIPLAYER
    }


    public ControllerManager cManager;
    public GameObject joystick;
    public GameObject kboard;
    public string keyboardName;
    List<jStick> jSticks;
    jStick keyboard;
    float inputDelay;//, inputTime;

    [SerializeField]
    GameObject leftObject;
    [SerializeField]
    GameObject centerObject;
    [SerializeField]
    GameObject rightObject;

    //float spriteOffset;
    public static string leftController = null;
    public static string rightController = null;

    //List<int> currentItems;
    
    //int currentItem = 1;

    // Use this for initialization
    void Start ()
    {
        //spriteOffset = 0;
        inputDelay = .2f;
        //inputTime = 0;
        jSticks = new List<jStick>();
        keyboard = new jStick();
        keyboardName = "kbo_";
        //Debug.Log("Number of controllers in manager: " + cManager.controllerList.Count);
        for (int i = 0; i < cManager.controllerList.Count; i++)
        {

            //currentItems.Add(1);
            jSticks.Add(new jStick());
            jSticks[i].gObj = Instantiate(joystick, transform.position, Quaternion.identity) as GameObject;
            jSticks[i].gObj.transform.localScale *= 30;
            jSticks[i].gObj.transform.parent = transform;
            jSticks[i].spriteOffset = 10 * i;
            jSticks[i].currentItem = 1;
        }
        keyboard.gObj = Instantiate(kboard, transform.position, Quaternion.identity)as GameObject;
        keyboard.gObj.transform.localScale *= 30;
        keyboard.gObj.transform.parent = transform;
        keyboard.spriteOffset = 10 * jSticks.Count;
        keyboard.currentItem = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {

        keyboard.inputTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && inputDelay < keyboard.inputTime)
        {
            keyboard.currentItem--;
            keyboard.inputTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.D) && inputDelay < keyboard.inputTime)
        {
            keyboard.currentItem++;
            keyboard.inputTime = 0;
        }

        keyboard.currentItem = Mathf.Clamp(keyboard.currentItem, 0, 2);

        switch (keyboard.currentItem)
        {
            case 0:
                keyboard.gObj.transform.position = new Vector3(leftObject.transform.position.x,
                                                                 leftObject.transform.position.y - keyboard.spriteOffset,
                                                                 leftObject.transform.position.z);
                break;
            case 1:
                keyboard.gObj.transform.position = new Vector3(centerObject.transform.position.x,
                                                                 centerObject.transform.position.y - keyboard.spriteOffset,
                                                                 centerObject.transform.position.z);
                break;
            case 2:
                keyboard.gObj.transform.position = new Vector3(rightObject.transform.position.x,
                                                                 rightObject.transform.position.y - keyboard.spriteOffset,
                                                                 rightObject.transform.position.z);
                break;
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (keyboard.selected == true)
            {
                //start level!!!!!
                SceneManager.LoadScene("Level1GrayBox", LoadSceneMode.Single);
                //break;
            }

            switch (keyboard.currentItem)
            {
                case 0:
                    if (leftController == null)
                    {
                        leftController = keyboardName;
                        keyboard.selected = true;
                    }
                    break;
                case 1:

                    break;
                case 2:
                    if (rightController == null)
                    {
                        rightController = keyboardName;
                        keyboard.selected = true;
                    }
                    break;
                    //jSticks[i].transform.localScale *= 1.3f;
            }
        }


        ////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////

        for (int i = 0; i < cManager.controllerList.Count; i++)
        {
            jSticks[i].inputTime += Time.deltaTime;
            //Debug.Log(cManager.controllerList[0].controllerName + "_rStickX");
            if (Input.GetAxis(cManager.controllerList[i].controllerName + "rStickX") < 0 &&
                inputDelay < jSticks[i].inputTime)
            {
                //move from center to left
                jSticks[i].currentItem--;
                jSticks[i].inputTime = 0;
                //move from right to center
            }
            if (Input.GetAxis(cManager.controllerList[i].controllerName + "rStickX") > 0 &&
                inputDelay < jSticks[i].inputTime)
            {
                jSticks[i].currentItem++;
                jSticks[i].inputTime = 0;
            }

            jSticks[i].currentItem = Mathf.Clamp(jSticks[i].currentItem, 0, 2);

           
            switch (jSticks[i].currentItem)
            {
                case 0:
                    jSticks[i].gObj.transform.position = new Vector3(leftObject.transform.position.x,
                                                                     leftObject.transform.position.y - jSticks[i].spriteOffset,
                                                                     leftObject.transform.position.z);
                    break;
                case 1:
                    jSticks[i].gObj.transform.position = new Vector3(centerObject.transform.position.x,
                                                                     centerObject.transform.position.y - jSticks[i].spriteOffset,
                                                                     centerObject.transform.position.z);
                    break;
                case 2:
                    jSticks[i].gObj.transform.position = new Vector3(rightObject.transform.position.x,
                                                                     rightObject.transform.position.y - jSticks[i].spriteOffset,
                                                                     rightObject.transform.position.z);
                    break;
            }

            if (Input.GetButtonDown(cManager.controllerList[i].controllerName + "start"))
            {
                if (jSticks[i].selected == true)
                {
                    //start level!!!!!
                    SceneManager.LoadScene("Level1GrayBox", LoadSceneMode.Single);
                    break;
                }

                switch (jSticks[i].currentItem)
                {
                    case 0:
                        if (leftController == null)
                        {
                            leftController = cManager.controllerList[i].controllerName;
                            jSticks[i].selected = true;
                        }
                        break;
                    case 1:

                        break;
                    case 2:
                        if (rightController == null)
                        {
                            rightController = cManager.controllerList[i].controllerName;
                            jSticks[i].selected = true;
                        }
                        break;
                        //jSticks[i].transform.localScale *= 1.3f;
                }
            }
        }
    }
}
