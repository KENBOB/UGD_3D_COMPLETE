using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attribute that stops the object from obtaining multiple scripts
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    Vector3 startingPos; //must be stored for absolute movement

    //range attribute to shows slider in inspector
    [Range(0, 1)] [SerializeField] float movementFactor; //0 for not moved, 1 for fully moved.
    // Start is called before the first frame update
    void Start()
    {
        //stored starting position
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
