using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameObject EnemyDeathFX;
    [SerializeField] Transform parent;
    
    //Create BoxColliders in code for all enemies containing this script
    void Start() {
        AddNonTriggerBoxCollider();
    }

    void AddNonTriggerBoxCollider(){
        //Adds box collider as game starts
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        //Ensures the box collider trigger check box is unchecked
        boxCollider.isTrigger = false;
    }

    //Detect when enemy object is hit and unproject with death animation at its position
    void OnParticleCollision(GameObject other) {
        print("Particles collided with enemy" + gameObject.name);
        //Initiate deathFX particles at its position in the world and do not rotate
        GameObject FXAnimation = Instantiate(EnemyDeathFX, transform.position, Quaternion.identity);
        //Set FXAnimation parent object to the parent
        //Moves the Enemy Death FX children from the 'enemy prefabs' to the empty object 'Spawned At Runtime'
        FXAnimation.transform.parent = parent;
        Destroy(gameObject);
    }
}
