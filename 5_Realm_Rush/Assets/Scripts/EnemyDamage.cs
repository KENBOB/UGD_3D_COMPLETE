using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;

    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        // print("I'm hit!");
        ProcessHit();
        if(hitPoints <= 0) {
            KillEnemy();
        }
    }

    void ProcessHit() {
        hitPoints = hitPoints -1;
        // print("Current hitpoints are " + hitPoints);
    }
    private void KillEnemy() {
        Destroy(gameObject);
    }
}
