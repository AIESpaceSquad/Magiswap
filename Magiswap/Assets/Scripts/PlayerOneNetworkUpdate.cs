using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// make required component here the component that
//will handle the physics of the player
//[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(PlayerController))]
public class PlayerOneNetworkUpdate : NetworkBehaviour
{
    NetworkIdentity _identity;
    PlayerController _controller;

    //declare physics component here
    //Rigidbody2D _rigid;


    // Use this for initialization
    void Start ()
    {
        _identity = GetComponent<NetworkIdentity>();
        _controller = GetComponent<PlayerController>();


        //init physics
        //_rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!_identity.isLocalPlayer)
        {
            transform.position += new Vector3(_controller.PlayerInput(), 0, 0);
            if (Input.GetKeyDown(KeyCode.E))
                return;
                //NetworkServer.ReplacePlayerForConnection() for changing player on singleplayer
            //if player isnt controlled by client
            return;
        }
        else //input goes here
        {
            transform.position += new Vector3(_controller.PlayerInput(),0,0); 
        }
	}
}
