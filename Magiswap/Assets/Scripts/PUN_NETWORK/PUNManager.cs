using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.UI;

enum LobbyState
{
    CreatePrivate,
    CreatePublic,
    JoinRandom,
    JoinPrivate,
    NotInRoom
}

public class PUNManager : Photon.PunBehaviour
{
    LobbyState lobbyState;
    public InputField inputField;
    public GameObject roomListPanel;
    public GameObject RoomViewPanel;
    //public GameObject inputField;
    RoomOptions roomOptions;
    bool debugRoom;
    string levelToLoadName;
    string buttonName;
    bool firstPlayerConnected;
    //bool publicGame;
	// Use this for initialization
	void Start ()
    {
        lobbyState = LobbyState.NotInRoom;
        roomOptions = new RoomOptions();
        PhotonNetwork.ConnectUsingSettings("0.1");
        
	}

	

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if(PhotonNetwork.inRoom)
        {
            int i = PhotonNetwork.playerList.Length - 1;
            GUILayout.Label(PhotonNetwork.room.name + " " + PhotonNetwork.GetPing().ToString());
            GUILayout.Label("Your in! You and " + i + " other people");
        }
        else
            GUILayout.Label("Couldn't find a room :(");
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinOrCreateRoom("Testy", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate("", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }


    // Update is called once per frame
    void Update ()
    {

    }

    public void SetNextLevel(string levelToLoad)
    {
        if (levelToLoad != null)
        {
            levelToLoadName = levelToLoad;
            inputField.interactable = true;
            inputField.ActivateInputField();
        }
        else
        {
            Debug.Log("no level to load");
        }
    }

    public void OnFindGame()
    { 
        // if you couldnt find an open room
        //ask to create a room
        if (lobbyState == LobbyState.NotInRoom)       //!PhotonNetwork.JoinRandomRoom() && lobbyState == LobbyState.NotInRoom)
        {
            lobbyState = LobbyState.JoinRandom;
            roomListPanel.SetActive(true);
            //lobbyState = LobbyState.NotInRoom;
        }
    }

    public void OnCreatePrivate()
    {

        //make this player have controller one here
        if (lobbyState == LobbyState.NotInRoom)
        {
            for(int i = 0; i < inputField.transform.childCount; i++)
            {
                inputField.transform.GetChild(i).gameObject.SetActive(true);
            }
            lobbyState = LobbyState.CreatePrivate;
        }
    }

    public void OnCreatePublic()
    {
        //make this player have controller one here

        if (lobbyState == LobbyState.NotInRoom)
        {
            for (int i = 0; i < inputField.transform.childCount; i++)
            {
                inputField.transform.GetChild(i).gameObject.SetActive(true);
            }
            lobbyState = LobbyState.CreatePublic;
        }
    }

    public void OnJoinPrivate()
    {

        //make this player have controller two here
        if (lobbyState == LobbyState.NotInRoom)
        {
            inputField.interactable = true;
            inputField.ActivateInputField();
            lobbyState = LobbyState.JoinPrivate;
        }
    }
    public void RecieveName(Text _buttonName)
    {
        buttonName = _buttonName.text;
    }
    public void JoinPublicGame()
    {
        //Debug.Log(buttonName);
        //make this player have controller two here

        PhotonNetwork.JoinRoom(buttonName);
    }

    public void OnStartGame()
    {
        PhotonNetwork.LoadLevel(levelToLoadName);
    }

    public void OnEndString()
    {
        switch(lobbyState)
        {
            case LobbyState.CreatePrivate:
                PhotonNetwork.CreateRoom("PRIVATE" + inputField.text);
                RoomViewPanel.SetActive(true);
                break;
            case LobbyState.CreatePublic:
                PhotonNetwork.CreateRoom(inputField.text);
                RoomViewPanel.SetActive(true);
                break;
            case LobbyState.JoinRandom:
                break;
            case LobbyState.JoinPrivate:
                PhotonNetwork.JoinRoom("PRIVATE" + inputField.text);
                RoomViewPanel.SetActive(true);
                break;
        }
    }
}
