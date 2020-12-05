using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class MyCubeEditor : MonoBehaviour {
    
   
    //[SerializeField] [Range(1f, 20f)] const float GridSize = 10;
    //Vector3 SnapToGridPos;
    Waypoint waypoint;

    //Grab waypoint component on awake
    private void Awake() {
        waypoint = GetComponent<Waypoint>();
    }

    void Start() {
        
    }

    void Update() {
        
        SnapToGrid();
        UpdateLabel();
    }

    //Snap blocks to grid without having to hold downt the ctrl key
    private void SnapToGrid() {
        
        // Grab grid size from way point connected to grid size function in wp script
        int GridSize = waypoint.GetGridSize();
        
        //Move to grid positions from waypoint script in GetGridPos funciton
        transform.position = new Vector3(
            waypoint.GetGridPos().x * GridSize,
            0f,
            waypoint.GetGridPos().y* GridSize);
    }

    //Update the grid number mesh labels (x,x) when moving blocks on the grid
    private void UpdateLabel() {
        int GridSize = waypoint.GetGridSize();
         //Grab the text component in child of cube and set it to positions "x,y"
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string LabelText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        textMesh.text = LabelText;
        //Change heirarchy cube object names to current positions
        gameObject.name = LabelText;
    }
}
