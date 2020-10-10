using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attribute that stops the object from obtaining multiple scripts
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    Vector3 startingPos; //must be stored for absolute movement
    //range attribute to shows slider in inspector
    //[Range(0, 1)] [SerializeField] remove exposing to inspector aka slider from 0 to 1
    float movementFactor; //0 for not moved, 1 for fully moved.
    
    // Start is called before the first frame update
    void Start()
    {
        //stored starting position
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //protects against period is zero for division
        //Mathf.Epsilon is the smallest thing we can represent closest to zero
        if (period <= Mathf.Epsilon){ return;}

        float cycles = Time.time / period; //grows continually from zero
        const float tau = Mathf.PI * 2; //about 6.28
        float rawSinWave = Mathf.Sin(cycles *  tau); //goes from -1 to +1
        //print(rawSinWave); //varying between -1 and +1
        
        movementFactor = rawSinWave / 2f + 0.5f; //calculate the directional movement as the cycle bounces from -1 to +1
        Vector3 offset = movementVector * movementFactor; //move in a vector direction by a factor or distance of x
        transform.position = startingPos + offset; //move the object from starting pt to new position
    }
}
