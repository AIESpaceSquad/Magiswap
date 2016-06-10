using UnityEngine;
using System.Collections;

public static class GameSettingsContainer {

    public enum playerControlSource
    {
        controller_keyboard = 0,
        controller_Gampad1  = 1,
        controller_Gampad2  = 2
    }

    public static string[] PCSText = { "keyboard", "Xbox360/XboxOne Gampad 1", "Xbox360/XboxOne Gampad 2" };

    //isSinglePlayer

    public static playerControlSource player1Controller = playerControlSource.controller_Gampad1;
    public static playerControlSource player2Controller = playerControlSource.controller_Gampad2;

    //game locality
    //host location

}
