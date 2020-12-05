using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class Pathfinder : MonoBehaviour {
    
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;

    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();  //List
    bool isRunning = true;
    Waypoint searchCenter = null; //Current searchCenter
    //New list waypoint creates it in the inspector to alter if needed
    List<Waypoint> path = new List<Waypoint>();

    //Grab the corresponding adjacent directions when searching for path
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    //Provide a simple way of getting the path for the enemy
    //Load all functions and send to enemy in start method
    public List<Waypoint> GetPath() {
        if (path.Count == 0) {
            CalculatePath();
        }
        return path;
    }

    //Calculate the path for the list
    private void CalculatePath() {
        LoadBlocks();
        ColorStartAndEnd();
        BreadthFirstSearch();
        CreatePath();
    }

    //Make the path starting from the end goal and reverse the path
    private void CreatePath() {
        SetAsPath(endWaypoint); //Cannot place tower on end waypoint
        
        Waypoint previous = endWaypoint.ExploredFrom;
        while(previous != startWaypoint) {
            //add intermediate waypoints and set to non-placeable
            
            SetAsPath(previous);
            
            //update the new previous node
            previous = previous.ExploredFrom;
        }
        //add start waypoint and set to non-placeable
        SetAsPath(startWaypoint);
        
        //reverse the list
        path.Reverse();
    }

    private void SetAsPath(Waypoint waypoint) {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    //Searches for the path and requests check for end node
    private void BreadthFirstSearch() {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0 && isRunning) {
            //Remove the parent from search queue
            searchCenter = queue.Dequeue();
            // print("Searching from: " + searchCenter);
            HaltIfEndFound();
            ExploreNeighbors();
            searchCenter.isExplored = true;
        }
        // print("Finished pathfinding?");
    }

    //Notifies the end has been reached
    private void HaltIfEndFound() {
        if(searchCenter == endWaypoint) {
            // print("Searching from end node, therefore stopping.");
            isRunning = false;
        }
    }

    //Halt if end is found and then explore neighbors
    private void ExploreNeighbors() {
        if(!isRunning) { return; }

        foreach (Vector2Int direction in directions) {
            //print(direction);
            //Take the start coordinate and add it to the direction by +1 or -1 in x and or y direction
            Vector2Int NeighborCoordinates = searchCenter.GetGridPos() + direction;
            
            //print("Exploring " + NeighborCoordinates);    //Print the 4 directions you are exploring
            //If it is contained in the dictionary queue it, if not do nothing
             if (grid.ContainsKey(NeighborCoordinates)) {
                QueueNewNeighbors(NeighborCoordinates);
            }
        }
    }

    //Queue up new neighbors to search for the end point
    private void QueueNewNeighbors(Vector2Int NeighborCoordinates) {
        Waypoint neighbor = grid[NeighborCoordinates];
        if(neighbor.isExplored || queue.Contains(neighbor)) {
            //do nothing
        } else {
            queue.Enqueue(neighbor);
            // print("Queueing " + neighbor);
            neighbor.ExploredFrom = searchCenter;
        }
    }

    //Color code the start and end points
    private void ColorStartAndEnd() {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
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
                waypoint.SetTopColor(Color.blue);
            }
        }
        print("Loaded " + grid.Count + " blocks.");
    }
}
