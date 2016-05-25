using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{
    ////for beta build only
    ////****
    public Button levelOneButton;
    public Button levelTwoButton;
    public string levelOneName;
    public string levelTwoName;
    bool levelOneSelected;
    float selectTimer, selectDeadZone, selectDelay;
    ////****

    //will load next level in build settings
    public void LoadLevel()
    {
        Scene temp = SceneManager.GetActiveScene();
        if(SceneManager.GetSceneAt(temp.buildIndex + 1).IsValid())
        {
            //SceneManager.LoadScene(temp.buildIndex + 1);
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
        SceneManager.LoadScene(levelName);

        levelName = "Scenes/" + levelName;
        Debug.Log("Made it to the load Level!");
        if (SceneManager.GetSceneByName(levelName).IsValid())
        {
            SceneManager.LoadScene(levelName);
            Debug.Log("loading level: " + levelName);
        }
        else
        {
            Debug.Log("No level with name " + levelName + ", check build settings");
        }
    }

    // Use this for initialization
    void Start()
    {
        levelOneSelected = true;
        selectTimer = 0;
        selectDeadZone = .02f;
        selectDelay = .25f;

        //SceneManager.LoadScene(levelOneName);
    }

    // Update is called once per frame
    void Update()
    {

        ////for beta build only
        ////****
        selectTimer += Time.deltaTime;

        if (levelOneSelected)
        {
            levelOneButton.transform.localScale = new Vector3(1.25f, 1.25f, 0);
            levelTwoButton.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            levelTwoButton.transform.localScale = new Vector3(1.25f, 1.25f, 0);
            levelOneButton.transform.localScale = new Vector3(1, 1, 0);
        }
        // if there is input from controllers, select other button
        if (Input.GetAxis("gp1_moveX") > selectDeadZone || Input.GetAxis("gp2_moveX") > selectDeadZone ||
           Input.GetAxis("gp1_moveX") < -selectDeadZone || Input.GetAxis("gp2_moveX") < -selectDeadZone)
        {
            if (selectTimer > selectDelay)
            {
                selectTimer = 0;
                levelOneSelected = !levelOneSelected;
            }
        }

        if (Input.GetButtonDown("gp1_jump") || Input.GetButtonDown("gp2_jump"))
        {
            if (levelOneSelected)
                LoadLevel(levelOneName);

            else
                LoadLevel(levelTwoName);
        }
        if (Input.GetButtonDown("gp1_back") || Input.GetButtonDown("gp2_back"))
        {
            LoadLevel("AlphaScene");
        }
        //****

    }
}