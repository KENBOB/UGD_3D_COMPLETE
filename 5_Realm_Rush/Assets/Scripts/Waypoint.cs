using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    
    Vector2Int gridPos;
    const int GridSize = 10;
    
    void Start() {
        
    }

    public Vector2Int GetGridPos() {

        // Grab the gridPos math calculations
        // Round the x position to 1 and multiply by 10 so it snaps through the script instead of the editor
        //Snap everything to grid by 10
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GridSize) * GridSize,
            Mathf.RoundToInt(transform.position.z / GridSize) * GridSize
        );
    }

    public int GetGridSize() {
        return GridSize;
    }
    
    void Update() {
        
    }
}
