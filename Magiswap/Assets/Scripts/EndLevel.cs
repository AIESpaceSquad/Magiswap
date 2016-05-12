using UnityEngine;
using System.Collections;
public class EndLevel : MonoBehaviour
{
    GameObject collidedPlayer;
    SceneLoader levelLoader;
    public GameObject sceneManager;
    public string levelToLoad;
	// Use this for initialization
	void Start ()
    {
        collidedPlayer = null;
        levelLoader = sceneManager.GetComponent<SceneLoader>();
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
            levelLoader.LoadLevel(levelToLoad);
        }
    }
}
