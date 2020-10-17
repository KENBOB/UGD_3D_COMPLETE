using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    //Scene Level Manager
    void startGame() {
        Invoke("ReloadScene" , 2f);
    }

    //Reloads Level 1 for now
    void ReloadScene() {
        SceneManager.LoadScene(1);
    }
}
