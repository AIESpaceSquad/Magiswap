using UnityEngine;
using System.Collections;

public class StartupModule : MonoBehaviour {

	void Awake()
    {
        listenerCharacter[] characters = FindObjectsOfType<listenerCharacter>();

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].playerNumber == 1)
            {
                characters[i].controllerNumber = (int)GameSettingsContainer.player1Controller;
            }
            else if (characters[i].playerNumber == 2)
            {
                characters[i].controllerNumber = (int)GameSettingsContainer.player2Controller;
            }
        }

        Destroy(this);
    }
}
