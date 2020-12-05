using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    
    [SerializeField] Color ExploredColor;

    //Public ok here as it is a data class
    public bool isExplored = false;
    public Waypoint ExploredFrom;
    public Waypoint neighbor;
    public bool isPlaceable = true;

    Vector2Int gridPos;
    const int GridSize = 10;

    void Start() {
        
    }

    void Update() {
        SetExploredColor();
    }

    //Grab the gridPos math calculations and Snap everything to grid by 10
    public Vector2Int GetGridPos() {
        // Round the x position to 1 and multiply by 10 so it snaps through the script instead of the editor
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GridSize),
            Mathf.RoundToInt(transform.position.z / GridSize)
        );
    }

    //Return the grid size
    public int GetGridSize() {
        return GridSize;
    }

    //Change the color of the top side of the cube to mark as explored
    public void SetExploredColor() {
        if(isExplored && ExploredFrom != null) {
            ExploredColor = Color.yellow;
            ExploredFrom.SetTopColor(ExploredColor);
        }
    }
    
    //Change the color on the top side of the cube
    public void SetTopColor(Color color) {
        // Find the top mesh renderer in all TOP child objects
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
        topMeshRenderer.material.color = color;
    }

    void OnMouseOver() {
        
        if (Input.GetMouseButtonDown(0)) {
            if (isPlaceable) {
                print("Clicked on " + gameObject.name + (" block."));
            } else {
                print("Can't place tower on " + gameObject.name);
            }
        }
    }
}
