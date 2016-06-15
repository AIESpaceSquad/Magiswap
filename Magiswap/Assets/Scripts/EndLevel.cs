using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EndLevel : MonoBehaviour
{
    GameObject collidedPlayer;
    //MainMenu or next level
    public string levelToLoad;
	// Use this for initialization
	void Start ()
    {
        collidedPlayer = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.transform.tag == "Player" && 
            collidedPlayer == null)
        {
            collidedPlayer = coll.gameObject;
        }
        else if(coll.transform.tag == "Player" && 
                collidedPlayer != coll.gameObject)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
