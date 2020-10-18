using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {
    
    

    int score;
    Text scoreText;

    //Grabs entire Text object component and sets the score to zero
    void Start() {
        DisplayScore();
    }
    
    public void DisplayScore(){
        //Find Text component
        scoreText = GetComponent<Text>();
        
        //Find Text bar of that component and evaluate to string on screen
        scoreText.text = score.ToString();
    }

    //Calculates score and updates it to string on screen
    public void ScoreHit(int scoreIncrease){
        score = score + scoreIncrease;
        scoreText.text = score.ToString();
    }
}
