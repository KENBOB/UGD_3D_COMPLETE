using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class MyCubeEditor : MonoBehaviour {
    
    [SerializeField] [Range(1f, 20f)] float GridSize = 10f;

    TextMesh textMesh;

    void Start() {
        
    }

    void Update() {

        Vector3 SnapPos;
        // Round the x position to 1 and multiply by 10 so it snaps through the script instead of the editor
        //Snap everything to grid by 10
        SnapPos.x = Mathf.RoundToInt(transform.position.x / GridSize) * GridSize;
        SnapPos.z = Mathf.RoundToInt(transform.position.z / GridSize) * GridSize;
        transform.position = new Vector3(SnapPos.x, 0f, SnapPos.z);

        //Grab the text component in child of cube and set it to positions "x,y"
        textMesh = GetComponentInChildren<TextMesh>();
        string LabelText = SnapPos.x / GridSize + "," + SnapPos.z / GridSize;
        textMesh.text = LabelText;
        //Change heirarchy cube object names to current positions
        gameObject.name = LabelText;
    }
}
