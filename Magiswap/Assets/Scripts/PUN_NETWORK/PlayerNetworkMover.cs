using UnityEngine;
using System.Collections;

public class PlayerNetworkMover : Photon.MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    public int controller;
	// Use this for initialization
	void Start ()
    {


	    if(photonView.isMine)
        {
            GetComponent<listenerCharacter>().enabled = true;
        }
        else
        {
            StartCoroutine("UpdateData");
        }
	}
	
    //public void OnSpawn()
    //{
    //
    //}

    IEnumerator UpdateDate()
    {
        while(true)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
            yield return null;
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
        }
    }

	// Update is called once per frame
	void Update ()
    {
	
	}
}
