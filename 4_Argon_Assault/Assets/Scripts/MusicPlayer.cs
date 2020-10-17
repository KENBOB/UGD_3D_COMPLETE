using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    //Trigger Game start and keep only one music bot playing
    private void Awake() {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        print("Number of music players in thise scene " + numMusicPlayers);
        //Call Scene Loader
        SendMessage("startGame");

        //Checks for multiple music bots and deletes extras
        if(numMusicPlayers > 1){
            Destroy(gameObject);
        } else {
            //Alternatively can say 'this' in place of 'gameObject'
            DontDestroyOnLoad(gameObject);
        }
    }
}
