using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    public float rotationSpeed = 100f;
    public float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;
    
    public AudioSource CurrentAudioSound;
    public AudioClip MainEngine;
    public AudioClip LevelSuccess;
    public AudioClip DeathClip;

    public Rigidbody rgbody;
    public ParticleSystem MainEngineAnimation;
    public ParticleSystem LevelCompleteAnimation;
    public ParticleSystem DeathAnimation;

    // [SerializeField] <- syntax serialized field can change in inspector, but not from other scripts
    // public can change from inspector and other scripts
    enum State {Alive, Dying, Transcending};
    [SerializeField] State CurrentStatus = State.Alive;

    bool CollisionsDisabled = true;
    
    // Start is called before the first frame update
    // Grab rocket body for detection and grab audiosource frame by frame
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
        //use debug cheat keys, but disable for player use on Dev Build
        if(Debug.isDebugBuild){
            DebugKeysToggle();
        }
        
    }

    // Collision detection for changing states
    void OnCollisionEnter(Collision collision){
        print("Collided");
        // ignore collisions when dead
        if (CurrentStatus != State.Alive || !CollisionsDisabled) {
            return;
        } 
        
        switch(collision.gameObject.tag){
            case "Friendly":
                // do nothing
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

    // State Changes
    private void StartSuccessSequence(){
        print("Finish");
        CurrentStatus = State.Transcending;
        CurrentAudioSound.Stop();
        CurrentAudioSound.PlayOneShot(LevelSuccess);
        LevelCompleteAnimation.Play();
        // invoke to load string to call load next scene after 1 second
        Invoke("LoadNextLevel", levelLoadDelay); // parameterise time
    }

    private void StartDeathSequence(){
        print("Hit Something Deadly");
        CurrentStatus = State.Dying;
        CurrentAudioSound.Stop();
        CurrentAudioSound.PlayOneShot(DeathClip);
        DeathAnimation.Play();
        Invoke("LoadFirstLevel", levelLoadDelay); // parameterise time
    }

    // Level Swapping
    private void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void LoadFirstLevel(){
        SceneManager.LoadScene(0);
    }

    // Debug Keys for testing
    private void DebugKeysToggle(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        } else if(Input.GetKeyDown(KeyCode.C)){
            // toggle collision
            CollisionsDisabled = !CollisionsDisabled;
        }
    }

    // Movement Controls
    private void ThrustInputResponse(){
        if(Input.GetKey(KeyCode.Space)){
            ApplyThrust();
        } else {
            StopThrustInput();    
        }
    }

    private void StopThrustInput(){
        CurrentAudioSound.Stop();
        MainEngineAnimation.Stop();
    }

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

        //rgbody.freezeRotation = true; // take manual control of rotation
        rgbody.angularVelocity = Vector3.zero; //remove angular rotation due to physics
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward * rotationThisFrame);
            print("rotate left");
        } 
        else if (Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward * rotationThisFrame);
            print("rotate right");
        }
       //rgbody.freezeRotation = false; // resume physics control of rotation
    }
}
