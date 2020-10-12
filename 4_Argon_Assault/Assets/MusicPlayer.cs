using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    [SerializeField] float LevelLoadDelay = 2f;

    private void Awake(){
        //alternatively can say 'this' in place of 'gameObject'
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        Invoke("LoadFirstScene", LevelLoadDelay);
    }

    void LoadFirstScene() {
        SceneManager.LoadScene(1);
    }
}
