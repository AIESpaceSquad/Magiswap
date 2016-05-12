using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    //will load next level in build settings
    public void LoadLevel()
    {
        Scene temp = SceneManager.GetActiveScene();
        if(SceneManager.GetSceneAt(temp.buildIndex + 1).IsValid())
        {
            SceneManager.LoadScene(temp.buildIndex + 1);
        }
        else
        {
            Debug.Log("Doesn't seem to be another level :(");
        }
    }

    public void LoadLevel(int i)
    {
        if (SceneManager.GetSceneAt(i).IsValid())
        {
            SceneManager.LoadScene(i);
        }
        else
        {
            Debug.Log("No level with index " + i + ", check build settings");
        }
    }

    public void LoadLevel(string levelName)
    {
        if(SceneManager.GetSceneByName(levelName).IsValid())
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.Log("No level with name " + levelName + ", check build settings");
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}