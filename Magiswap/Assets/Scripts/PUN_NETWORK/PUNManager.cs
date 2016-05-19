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
    //public GameObject inputField;
    RoomOptions roomOptions;
    bool debugRoom;
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
            GUILayout.Label(PhotonNetwork.room.name + " " + PhotonNetwork.GetPing().ToString());
            GUILayout.Label("Your in!");
        }
        else
            GUILayout.Label("Couldn't find a room :(");
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinOrCreateRoom("Testy", roomOptions, TypedLobby.Default);
    }


    // Update is called once per frame
    void Update ()
    {
	    
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
       // Joined a room! Enjoy!
        //else
        //{
        //    lobbyState = LobbyState.JoinRandom;
        //}


    }

    public void OnCreatePrivate()
    {
        if(lobbyState == LobbyState.NotInRoom)
        {
            inputField.interactable = true;
            inputField.ActivateInputField();
            lobbyState = LobbyState.CreatePrivate;
        }
    }

    public void OnCreatePublic()
    {
        if (lobbyState == LobbyState.NotInRoom)
        {
            inputField.interactable = true;
            inputField.ActivateInputField();
            lobbyState = LobbyState.CreatePublic;
        }
    }

    public void OnJoinPrivate()
    {
        if (lobbyState == LobbyState.NotInRoom)
        {
            inputField.interactable = true;
            inputField.ActivateInputField();
            lobbyState = LobbyState.JoinPrivate;
        }
    }

    public void OnEndString()
    {
        switch(lobbyState)
        {
            case LobbyState.CreatePrivate:
                PhotonNetwork.CreateRoom("PRIVATE" + inputField.text);
                break;
            case LobbyState.CreatePublic:
                PhotonNetwork.CreateRoom(inputField.text);
                break;
            case LobbyState.JoinRandom:
                break;
            case LobbyState.JoinPrivate:
                PhotonNetwork.JoinRoom("PRIVATE" + inputField.text);
                break;
        }
    }
}
