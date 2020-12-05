// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Parameters of each tower
    [SerializeField] Transform objectToPan = null;
    [SerializeField] float attackRange = 10f;
    [SerializeField] ParticleSystem projectileParticle = null;

    //State of each tower
    Transform targetEnemy = null;

    //Move the turrets to look at the enemy, shoot it, and stop when enemy is dead
    void Update()
    {
        SetTartgetEnemy();
        if(targetEnemy) {
            objectToPan.LookAt(targetEnemy);
            FireAtEnemy();
        } else {
            Shoot(false);
        }
    }

    //Find the length of enemies in the scene and pan to the winner in the update
    private void SetTartgetEnemy() {
        //Get the collection of things
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if(sceneEnemies.Length == 0) { return; }

        //Assume the first is the winner
        Transform closestEnemy = sceneEnemies[0].transform;

        //Update the winner
        foreach (EnemyDamage testEnemy in sceneEnemies) {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        //Choose the closest enemy to be panned to and fire upon
        targetEnemy = closestEnemy;
    }

    //Gather the distance information from both enemies, compare them, and select the shortest distance
    private Transform GetClosest(Transform transformA, Transform transformB) {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if(distToA < distToB) {
            return transformA;
        } 
        return transformB;
    }

    //Detect if enemy is in range to be fired upon
    private void FireAtEnemy() {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
        if (distanceToEnemy <= attackRange) {
            Shoot(true);
        }
        else {
            Shoot(false);
        }
    }

    //Swap the turret firing on and off when enemy dies
    private void Shoot(bool isActive) {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }

}
