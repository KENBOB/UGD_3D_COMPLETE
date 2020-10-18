using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    //Creates inspector element for placing explosion prefab
    [Tooltip("FX prefab on player")][SerializeField] GameObject PlayerDeathFX;
    [Tooltip("In seconds")][SerializeField] float LevelLoadDelay = 2f;

    //Trigger Physics Detection and calls explosion animation
    void OnTriggerEnter(Collider other) {
        print("Player triggered something");
        StartDeathSequence();

        //Set explosion animation to visible on the screen
        PlayerDeathFX.SetActive(true);
        
    }

    //Freezes controls, calls scene loader
    private void StartDeathSequence() {
        print("Player is dying");

        //Call Player Controller script function to freeze controls
        SendMessage("OnPlayerDeath");
        
        //Call Scene Loader script on a delay
        Invoke("callSceneLoader", LevelLoadDelay);
    }

    private void callSceneLoader() {
        //Calls Scene Loader function
        SendMessage("ReloadScene");
    }
}
