using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rgbody;
    public float rotationSpeed = 90;
    AudioSource RocketThrust;
    
    // Start is called before the first frame update
    void Start() {
        rgbody = GetComponent<Rigidbody>();
        RocketThrust = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        Thrust();
        Rotate();
    }

    private void Thrust(){
        if(Input.GetKey(KeyCode.Space)){
            rgbody.AddRelativeForce(Vector3.up);
            print("Thrusting");

        if(!RocketThrust.isPlaying){
            RocketThrust.Play();
            } 
        } else {
            RocketThrust.Stop();
        }
    }
    
    private void Rotate(){

        rgbody.freezeRotation = true; //take manual control of rotation

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            print("rotate left");
        } 
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed);
            print("rotate right");
        }

        rgbody.freezeRotation = false; //resume physics control of rotation
    }
}
