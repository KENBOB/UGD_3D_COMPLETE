using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rgbody;
    AudioSource RocketThrust;
    public float rotationSpeed = 100f;
    public float mainThrust = 100f;
    //serialized field can change in inspector, but not from other scripts
    //public can change from inspector and other scripts
    
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

    void OnCollisionEnter(Collision collision){
        print("Collided");
        switch(collision.gameObject.tag){
            case "Friendly":
                //do nothing
                print("OK");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("dead");
                break;
        }
    }

    private void Thrust(){
        if(Input.GetKey(KeyCode.Space)){
            rgbody.AddRelativeForce(Vector3.up * mainThrust);
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
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * rotationThisFrame);
            print("rotate left");
        } 
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationThisFrame);
            print("rotate right");
        }

        rgbody.freezeRotation = false; //resume physics control of rotation
    }
}
