using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkLobbyManager
{
    //**** custom ui for lobby
    //public GameObject leftCharacter;
    //public GameObject rightCharacter;
    //public GameObject playerSpawn;
    //*****
    //***** for singleplayer
    //public GameObject secondCharacter;
    //NetworkIdentity _identity;
    //GameObject player;
    //*****
    // Use this for initialization
    void Start ()
    {

        //_identity = GetComponent<NetworkIdentity>();
        //secondCharacter = new GameObject();
        //playerSpawn = new GameObject();
        //leftCharacter.GetComponent<PlayerController>().controllerName = CharacterSelector.leftController;
        //rightCharacter.GetComponent<PlayerController>().controllerName = CharacterSelector.rightController;
    }

    // Update is called once per frame
    void Update ()
    {
	
	}

    //public override void OnStartClient(NetworkClient client)
    //{
    //    base.OnStartClient(client);
    //}

    

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //overloading the on server add player because i dont trust unity anymore....
        GameObject player = Instantiate(lobbyPlayerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        ///////////////////////////////////////////////////////////////////////////////////////
        //base.OnServerAddPlayer(conn, playerControllerId);
        
        //Debug.Log(CharacterSelector.leftController +" " + CharacterSelector.rightController);

        //if (CharacterSelector.leftController != null &&
        //    CharacterSelector.rightController == null)

        //{
        //    if (GameObject.Find("Player start") == null)
        //        Debug.Log("no Player start found");
        //    playerSpawn = new GameObject();
        //    playerSpawn.transform.position = GameObject.Find("Player start").GetComponent<Transform>().position;

        //    secondCharacter = Instantiate(playerPrefab, playerSpawn.transform.position, Quaternion.identity) as GameObject;
        //    //secondCharacter.GetComponent<PlayerController>().controllerName = CharacterSelector.rightController;
        //    //Debug.Log(secondCharacter.GetComponent<PlayerController>().controllerName);
        //}

        //if (CharacterSelector.leftController == null &&
        //  CharacterSelector.rightController != null)

        //{
        //    if (GameObject.Find("Player start") == null)
        //        Debug.Log("no Player start found");
        //    playerSpawn = new GameObject();
        //    playerSpawn.transform.position = GameObject.Find("Player start").GetComponent<Transform>().position;

        //    secondCharacter = Instantiate(playerPrefab, playerSpawn.transform.position, Quaternion.identity) as GameObject;
        //}

        //if (CharacterSelector.leftController != null &&
        //   CharacterSelector.rightController != null)
        //{
        //    if (GameObject.Find("Player start") == null)
        //        Debug.Log("no Player start found");
        //    playerSpawn = new GameObject();
        //    playerSpawn.transform.position = GameObject.Find("Player start").GetComponent<Transform>().position;

        //    secondCharacter = Instantiate(playerPrefab, playerSpawn.transform.position, Quaternion.identity) as GameObject;
        //}
        ////base.OnServerAddPlayer(conn, playerControllerId);
        ////if there is one player on the left character
        //if (CharacterSelector.leftController != null &&
        //    CharacterSelector.rightController == null)
        //{
        //    player = rightCharacter;
        //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //    player = leftCharacter;
        //    //NetworkServer.SpawnObjects();
        //    NetworkServer.ReplacePlayerForConnection(conn, player, playerControllerId);
        //    //NetworkServer.ReplacePlayerForConnection(conn, player, playerControllerId);
        //    Debug.Log("SinglePlayer with player on left side");
        //}
        ////if there is one player in right character
        //else if (CharacterSelector.leftController == null &&
        //        CharacterSelector.rightController != null)
        //{
        //    player = rightCharacter;
        //}
        ////if two players are connected
        //else if (CharacterSelector.leftController != null &&
        //        CharacterSelector.rightController != null)
        //{

        //}
    }

}
