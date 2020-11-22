using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // [SerializeField] List<Waypoint> path = null;

    //Start is called before the first frame update
    void Start() {
        
        //use only if you have only 1 pathfinder
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
        //print("Hey I'm back at Start.");
    }

    //print the path, send the path in and use it in the while loop
    IEnumerator FollowPath(List<Waypoint> path) {
        print("Starting Patrol.");
        foreach (Waypoint waypoint in path) {
            //Set enemy position to the next way point position in List
            transform.position = waypoint.transform.position;
            //print("Visiting block: " + waypoint.name);

            //Pause there for x seconds 
            yield return new WaitForSeconds(1f);
        }
        print("Ending Patrol.");
    }
}
