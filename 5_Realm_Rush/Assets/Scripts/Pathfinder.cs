using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    void Start() {
        LoadBlocks();
    }

    
    void Update() {
        
    }

    //Builds a dictionary of all the blocks at their positions on the grid
    private void LoadBlocks() {
        //Grabs all items with waypoint script attached to it
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints) {

            //overlapping blocks
            var gridPos = waypoint.GetGridPos();
            //bool isOverlapping = grid.ContainsKey(gridPos);
            if(grid.ContainsKey(gridPos)) {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            } else {
                //add to dictionary
                grid.Add(gridPos, waypoint);
            }
        }
        print("Loaded " + grid.Count + " blocks.");
    }
}
