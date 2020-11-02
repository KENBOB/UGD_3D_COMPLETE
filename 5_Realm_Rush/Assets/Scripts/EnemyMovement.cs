using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] List<Waypoint> path = null;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(FollowPath());
        print("Hey I'm back at Start.");
    }

    IEnumerator FollowPath() {
        print("Starting Patrol.");

        foreach (Waypoint waypoint in path) {
            //Set enemy position to the next way point position in List
            transform.position = waypoint.transform.position;
            print("Visiting block: " + waypoint.name);
            //Pause there for x seconds
            yield return new WaitForSeconds(1f);
            
        }
        print("Ending Patrol.");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
