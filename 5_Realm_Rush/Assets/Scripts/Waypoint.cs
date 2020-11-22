using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    
    [SerializeField] Color ExploredColor;

    //Public ok here as it is a data class
    public bool isExplored = false;
    public Waypoint ExploredFrom;
    public Waypoint neighbor;

    Vector2Int gridPos;
    const int GridSize = 10;

    void Update() {
        SetExploredColor();
    }

    public Vector2Int GetGridPos() {

        // Grab the gridPos math calculations and Snap everything to grid by 10
        // Round the x position to 1 and multiply by 10 so it snaps through the script instead of the editor
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GridSize),
            Mathf.RoundToInt(transform.position.z / GridSize)
        );
    }

    public int GetGridSize() {
        return GridSize;
    }

    public void SetExploredColor() {
        if(isExplored && ExploredFrom != null) {
            ExploredColor = Color.yellow;
            ExploredFrom.SetTopColor(ExploredColor);
        }
    }
    
    public void SetTopColor(Color color) {
        // Find the top mesh renderer in all TOP child objects
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }
}
