using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    public Rigidbody rgbody;
    public AudioSource CurrentAudioSound;
    public float rotationSpeed = 100f;
    public float mainThrust = 100f;
    public AudioClip MainEngine;
    public AudioClip LevelSuccess;
    public AudioClip DeathClip;

    public ParticleSystem MainEngineAnimation;
    public ParticleSystem LevelCompleteAnimation;
    public ParticleSystem DeathAnimation;
    //[SerializeField] <- syntax serialized field can change in inspector, but not from other scripts
    //public can change from inspector and other scripts
    enum State {Alive, Dying, Transcending};
    [SerializeField] State CurrentStatus = State.Alive;
    
    // Start is called before the first frame update
    void Start() {
        rgbody = GetComponent<Rigidbody>();
        CurrentAudioSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if(CurrentStatus == State.Alive){
            ThrustInputResponse();
            RotateInputResponse();
        }
    }

    //Collision detection for changing states
    void OnCollisionEnter(Collision collision){
        print("Collided");
        // ignore collisions when dead
        if (CurrentStatus != State.Alive) {
            return;
        } 
        
        switch(collision.gameObject.tag){
            case "Friendly":
                //do nothing
                print("OK");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    //State Changes
    private void StartSuccessSequence(){
        print("Finish");
        CurrentStatus = State.Transcending;
        CurrentAudioSound.Stop();
        CurrentAudioSound.PlayOneShot(LevelSuccess);
        LevelCompleteAnimation.Play();
        //invoke to load string to call load next scene after 1 second
        Invoke("LoadNextLevel", 1f); //parameterise time
    }

    private void StartDeathSequence(){
        print("Hit Something Deadly");
        CurrentStatus = State.Dying;
        CurrentAudioSound.Stop();
        CurrentAudioSound.PlayOneShot(DeathClip);
        DeathAnimation.Play();
        Invoke("LoadFirstLevel", 1f); //parameterise time
    }

    //Level Swapping
    private void LoadNextLevel(){
        SceneManager.LoadScene(1);
    }
    private void LoadFirstLevel(){
        SceneManager.LoadScene(0);
    }

    private void ThrustInputResponse(){
        if(Input.GetKey(KeyCode.Space)){
            ApplyThrust();
        } else {
            CurrentAudioSound.Stop();
            MainEngineAnimation.Stop();
        }
    }

    //Movement Controls
    private void ApplyThrust(){
        rgbody.AddRelativeForce(Vector3.up * mainThrust);
        print("Thrusting");

        if(!CurrentAudioSound.isPlaying){
            CurrentAudioSound.PlayOneShot(MainEngine);
        }
        if (!MainEngineAnimation.isPlaying) {
            MainEngineAnimation.Play();
        }
    }
    
    private void RotateInputResponse(){

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
