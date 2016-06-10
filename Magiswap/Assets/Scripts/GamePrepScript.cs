using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePrepScript : MonoBehaviour {

    //UItext stuff
    //main menu texts (for highligting)
    [SerializeField]
    Text levelText;
    [SerializeField]
    Text player1Text;
    [SerializeField]
    Text player2Text;
    [SerializeField]
    Text startText;
    //main menu change text
    [SerializeField]
    Text levelParam;
    [SerializeField]
    Text player1Param;
    [SerializeField]
    Text player2Param;
    //submenus
    //levels
    [SerializeField]
    Text levelTitle;
    [SerializeField]
    Text levelOne;
    //controllers
    [SerializeField]
    Text contrTitle;
    [SerializeField]
    Text contrKeyboard;
    [SerializeField]
    Text contrGamepad1;
    [SerializeField]
    Text contrGamepad2;
    //menu lengths
    int mainLength = 4;
    int subLevelsLength = 1;
    int subControllersLength = 3;

    //loadable levels (shoud have the lenngth of subLevelsLength)
    static string[] levels = { "_level1" };
    static string[] levelsVisible = { "(1) Forest" };

    //other
    float controllerDeadzone = 0.2f;
    float controllerCooldown = 0.25f;
    float controllerRemainingCooldown = 0.0f;

    enum curentMenu
    {
        cm_main = 0,
        cm_levels = 1,
        cm_controller = 2
    }

    //current indexes
    curentMenu CurrentMenu;
    int mainIndex;
    int subIndex;

    //param storage
    int readiedLevelIndex;
    int player1SavedIndex;
    int player2SavedIndex;

    // Use this for initialization
    void Start () {
        //index defaults
        CurrentMenu = curentMenu.cm_main;
        mainIndex = 0;
        subIndex  = 0;
        //param defaults
        readiedLevelIndex = 0;
        player1SavedIndex = 1;
        player2SavedIndex = 2;
        //other
        levelText.fontStyle = FontStyle.Bold;
        levelParam.text = levelsVisible[0];
        player1Param.text = GameSettingsContainer.PCSText[1];
        player2Param.text = GameSettingsContainer.PCSText[2];
    }
	
	// Update is called once per frame
	void Update () {
        if (controllerRemainingCooldown > 0.0f)
        {
            controllerRemainingCooldown -= Time.deltaTime;
        }
        else
        {
            if (ControllerHandler.MenuControllerGetYAxis() > controllerDeadzone)
            {
                if (CurrentMenu == curentMenu.cm_main)
                {
                    changeIndex(mainIndex - 1);
                }
                else
                {
                    changeIndex(subIndex - 1);
                }
                controllerRemainingCooldown = controllerCooldown;
            }
            else if (ControllerHandler.MenuControllerGetYAxis() < -controllerDeadzone)
            {
                if (CurrentMenu == curentMenu.cm_main)
                {
                    changeIndex(mainIndex + 1);
                }
                else
                {
                    changeIndex(subIndex + 1);
                }
                controllerRemainingCooldown = controllerCooldown;
            }
            else if (ControllerHandler.MenuControllerGetActivateButton())
            {
                activateCurrentItem();
                controllerRemainingCooldown = controllerCooldown;
            }
            else if (ControllerHandler.MenuControllerGetReturnButton())
            {
                cancelCurrentItem();
                controllerRemainingCooldown = controllerCooldown;
            }
        }
        
	}

    void changeIndex(int in_newIndex)
    {
        int resolvedIndex = in_newIndex;
        switch (CurrentMenu)
        {
            case curentMenu.cm_main:
                if (in_newIndex >= mainLength)
                {
                    resolvedIndex = 0;
                }
                else if (in_newIndex < 0)
                {
                    resolvedIndex = mainLength - 1;
                }
                
                switch (mainIndex)
                {
                    case 0:
                        levelText.fontStyle = FontStyle.Normal;
                        break;
                    case 1:
                        player1Text.fontStyle = FontStyle.Normal;
                        break;
                    case 2:
                        player2Text.fontStyle = FontStyle.Normal;
                        break;
                    case 3:
                        startText.fontStyle = FontStyle.Normal;
                        break;
                }

                switch (resolvedIndex)
                {
                    case 0:
                        levelText.fontStyle = FontStyle.Bold;
                        break;
                    case 1:
                        player1Text.fontStyle = FontStyle.Bold;
                        break;
                    case 2:
                        player2Text.fontStyle = FontStyle.Bold;
                        break;
                    case 3:
                        startText.fontStyle = FontStyle.Bold;
                        break;
                }

                mainIndex = resolvedIndex;

                break;
            case curentMenu.cm_levels:

                //ignoreing this now as we only have one level

                break;
            case curentMenu.cm_controller:

                if (in_newIndex >= subControllersLength)
                {
                    resolvedIndex = 0;
                }
                else if (in_newIndex < 0)
                {
                    resolvedIndex = subControllersLength - 1;
                }

                    contrKeyboard.fontStyle = FontStyle.Normal;
                    contrGamepad1.fontStyle = FontStyle.Normal;
                    contrGamepad2.fontStyle = FontStyle.Normal;

                switch (resolvedIndex)
                {
                    case 0:
                        contrKeyboard.fontStyle = FontStyle.Bold;
                        break;
                    case 1:
                        contrGamepad1.fontStyle = FontStyle.Bold;
                        break;
                    case 2:
                        contrGamepad2.fontStyle = FontStyle.Bold;
                        break;
                }

                subIndex = resolvedIndex;

                break;
        }
    }

    void changeMenu(curentMenu in_newMenu, int in_previousIndex = 0)
    {
        CurrentMenu = in_newMenu;
        switch (in_newMenu)
        {
            case curentMenu.cm_main:

                levelTitle.gameObject.SetActive(false);
                levelOne.gameObject.SetActive(false);

                contrTitle.gameObject.SetActive(false);
                contrKeyboard.gameObject.SetActive(false);
                contrGamepad1.gameObject.SetActive(false);
                contrGamepad2.gameObject.SetActive(false);

                mainIndex = in_previousIndex;
                changeIndex(mainIndex);
                break;
            case curentMenu.cm_levels:
                levelTitle.gameObject.SetActive(true);
                levelOne.gameObject.SetActive(true);

                subIndex = in_previousIndex;
                changeIndex(subIndex);
                break;
            case curentMenu.cm_controller:
                contrTitle.gameObject.SetActive(true);
                contrKeyboard.gameObject.SetActive(true);
                contrGamepad1.gameObject.SetActive(true);
                contrGamepad2.gameObject.SetActive(true);

                subIndex = in_previousIndex;
                changeIndex(subIndex);
                break;
        }
    }

    void activateCurrentItem()
    {
        switch (CurrentMenu)
        {
            case curentMenu.cm_main:
                switch (mainIndex)
                {
                    case 0:
                        changeMenu(curentMenu.cm_levels, readiedLevelIndex);
                        break;
                    case 1:
                        changeMenu(curentMenu.cm_controller, player1SavedIndex);
                        break;
                    case 2:
                        changeMenu(curentMenu.cm_controller, player2SavedIndex);
                        break;
                    case 3:
                        StartGame();
                        break;
                }

                break;
            case curentMenu.cm_levels:
                //case sub index
                readiedLevelIndex = subIndex;
                changeMenu(curentMenu.cm_main, mainIndex);
                levelParam.text = levelsVisible[subIndex];
                break;
            case curentMenu.cm_controller://add swaping of controllers if the other player already has the selectec controller;
                if (mainIndex == 1)
                {
                    player1SavedIndex = subIndex;
                    changeMenu(curentMenu.cm_main, mainIndex);
                    player1Param.text = GameSettingsContainer.PCSText[subIndex];
                }
                else if (mainIndex == 2)
                {
                    player2SavedIndex = subIndex;
                    changeMenu(curentMenu.cm_main, mainIndex);
                    player2Param.text = GameSettingsContainer.PCSText[subIndex];
                }

                break;
        }
    }

    void cancelCurrentItem()
    {
        switch(CurrentMenu)
        {
            case curentMenu.cm_main:
                //return to main scene
                break;
            case curentMenu.cm_levels:
                changeMenu(curentMenu.cm_main, mainIndex);
                break;
            case curentMenu.cm_controller:
                changeMenu(curentMenu.cm_main, mainIndex);
                break;
        }
    }

    void StartGame()
    {
        GameSettingsContainer.player1Controller = (GameSettingsContainer.playerControlSource)player1SavedIndex;
        GameSettingsContainer.player2Controller = (GameSettingsContainer.playerControlSource)player2SavedIndex;
        //go to level
    }
}
