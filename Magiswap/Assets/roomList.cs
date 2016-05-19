using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class roomList : MonoBehaviour
{
    PUNManager pManager;
    public Button buttonPrefab;
    float buttonYOffset;
    public Transform buttonPos;
	// Use this for initialization
	void Start ()
    {
        buttonYOffset = buttonPrefab.GetComponent<RectTransform>().rect.height;
        pManager = FindObjectOfType<PUNManager>();

        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            //if the game is private or if there are mre than one other players
            if(room.name.Substring(0,7) == "PRIVATE" || room.playerCount > 1)
            {
                return;
            }
            
            CreateButton(room.name, buttonYOffset);
            buttonYOffset += buttonYOffset;
        }
	}


    void CreateButton(string roomName, float yOffset)
    {
        Vector3 tempLocation = new Vector3(buttonPos.transform.position.x,
                                           buttonPos.transform.position.y - yOffset, 
                                           buttonPos.transform.position.z);
        
        Button temp = Instantiate(buttonPrefab, tempLocation, Quaternion.identity) as Button;
        temp.GetComponentInChildren<Text>().text = roomName;//temp.GetComponent<Text>().text = roomName;
        temp.transform.parent = this.transform;
    }
	// Update is called once per frame
	void Update ()
    {
	
	}
}
