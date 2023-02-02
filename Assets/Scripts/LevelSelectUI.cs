using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{

    //Build index of the current-oaded scene
    private int currentScene = 0;

    //Current scene's level view camera, if any
    private GameObject levelViewCamera;

    //Current ongoing scene loading operation, if any
    private AsyncOperation currentLoadOperation;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //If we have a current load operation and it's done
        if(currentLoadOperation != null && currentLoadOperation.isDone){
            //We're done loading the scene
            currentLoadOperation = null;

            //Get the level view camera from the scene
            levelViewCamera = GameObject.Find("Level View Camera");

            //Log an error if we couldn't find it
            if(levelViewCamera == null){
                Debug.LogError("Couldn't find 'Level View Camera' in scene " + currentScene);
            }


        }
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        GUILayout.Label("OBSTACLE COURSE");

        //if this isn't the main menu
        if(currentScene != 0){
            GUILayout.Label("Current vieving level " + currentScene);

            //Show a PLAY button
            if(GUILayout.Button("PLAY")){
                
                //If the button is clicked, start playing the level
                PlayCurrentLevel();
            }
        }else{
            //If this is the main menu
            GUILayout.Label("Select a level to view it");

            //Starting at scene build index 1, loop through all remaining scenes
            for(int i = 1; i < SceneManager.sceneCountInBuildSettings; i++){
                //Show a button for each scene
                if(GUILayout.Button("Level " + i)){
                    //if that button is clicked, and we're not waiting for a scene to load
                    if(currentLoadOperation == null){
                        //Load the scene
                        currentLoadOperation = SceneManager.LoadSceneAsync(i);
                        currentScene = i;
                    }
                }
            }
            
        }
    }

    //Start playing the current level
    private void PlayCurrentLevel(){
        //Deactivate the level view camera
        levelViewCamera.SetActive(false);

        //Try to find the player in the scene
        GameObject player = GameObject.Find("Player");

        //Throw an error if we couldn't find it
        if(player == null){
            Debug.LogError("Couldn't find 'Player' in scene " + currentScene);
        }else{
            //If we found the player, activate it
            var playerScript = player.GetComponent<Player>();
            playerScript.enabled = true;

            //Through the player script, acces the camera and activate it
            playerScript.cam.enabled = true;

            //Destroy self (It will be recreated when the scene is reloaded)
            Destroy(this.gameObject);
        }

        
    }
}
