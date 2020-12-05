using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)]
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] GameObject enemyPrefab = null;

    void Start() {
        StartCoroutine(RepeatedlySpawnEnemies());
    }

    //Co-routine spawner
    IEnumerator RepeatedlySpawnEnemies() {
        //Spawn Enemy and wait in a permanent loop
        while(true) {
            // print("Spawning");
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
        
}
