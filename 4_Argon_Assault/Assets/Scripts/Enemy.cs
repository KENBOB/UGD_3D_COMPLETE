using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject EnemyDeathFX;
    [SerializeField] Transform parent;
    
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hitPoints = 10;
    ScoreBoard scoreBoard;

    //Create BoxColliders in code for all enemies containing this script
    void Start() {
        AddNonTriggerBoxCollider();
        
        //Adds reference to the scoreboard object
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void AddNonTriggerBoxCollider(){
        //Adds box collider as game starts
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        
        //Ensures the box collider trigger check box is unchecked
        boxCollider.isTrigger = false;
    }

    //Detect when enemy object is hit and if the health is at 0, execute KillEnemy routine
    void OnParticleCollision(GameObject other) {
        print("Particles collided with enemy" + gameObject.name);
        ProcessHit();
        if (hitPoints <= 1) {
            KillEnemy();
        }
    }

    //Update hits dealt to enemies
    private void ProcessHit() {
        //Allows for user to change score on each enemy as needed
        scoreBoard.ScoreHit(scorePerHit);
        hitPoints = hitPoints - 1;
        //todo consider hit FX function
    }

    //Destroy enemy and project death animation at its position
    private void KillEnemy() {
        //Initiate deathFX particles at its position in the world and do not rotate
        GameObject FXAnimation = Instantiate(EnemyDeathFX, transform.position, Quaternion.identity);
        
        //Moves the Enemy Death FX children from the 'enemy prefabs' to the empty object 'Spawned At Runtime'
        FXAnimation.transform.parent = parent;  //Set FXAnimation parent object to the parent
        Destroy(gameObject);
    }
}
