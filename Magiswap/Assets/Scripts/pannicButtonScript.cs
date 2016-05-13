using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pannicButtonScript : MonoBehaviour {

    static bool isLoaded = false;

	// Use this for initialization
	void Start () {
        if (isLoaded)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            DontDestroyOnLoad(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("gp1_back") || Input.GetButtonDown("gp2_back"))
        {
            SceneManager.LoadScene("AlphaScene");
        }
	
	}
}
