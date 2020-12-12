using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab = null;
    [SerializeField] ParticleSystem deathParticlePrefab = null;

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
        if(hitPoints >= 1) {
            hitParticlePrefab.Play();
        }
        // print("Current hitpoints are " + hitPoints);
    }
    private void KillEnemy() {
        var deathfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        deathfx.Play();
        Destroy(gameObject);
    }
}
