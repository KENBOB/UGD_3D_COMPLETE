using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rgbody;

    // Start is called before the first frame update
    void Start() {
        rgbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        ProcessInput();
    }
    private void ProcessInput(){
        if(Input.GetKey(KeyCode.Space)){
            rgbody.AddRelativeForce(Vector3.up);
            print("Thrusting");
        } 
        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
            print("rotate left");
        } 
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
            print("rotate right");
        }
    }
}
