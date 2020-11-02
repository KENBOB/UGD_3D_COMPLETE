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

    private void Awake() {
        waypoint = GetComponent<Waypoint>();
    }

    void Start() {
        
    }

    void Update() {
        
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid() {
        
        // Grab grid size from way point connected to grid size function in wp script
        int GridSize = waypoint.GetGridSize();
        
        //Move to grid positions from waypoint script in GetGridPos funciton
        transform.position = new Vector3(waypoint.GetGridPos().x, 0f, waypoint.GetGridPos().y);
    }

    private void UpdateLabel() {
        int GridSize = waypoint.GetGridSize();
         //Grab the text component in child of cube and set it to positions "x,y"
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string LabelText = waypoint.GetGridPos().x / GridSize + "," + waypoint.GetGridPos().y / GridSize;
        textMesh.text = LabelText;
        //Change heirarchy cube object names to current positions
        gameObject.name = LabelText;
    }
}
