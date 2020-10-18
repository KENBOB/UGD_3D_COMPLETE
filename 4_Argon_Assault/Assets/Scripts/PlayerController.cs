using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    //todo work-out why sometimes slow on first play of scene

    [Header("General")]
    [Tooltip("In ms^1")][SerializeField] float controlSpeed = 40f;
    [Tooltip("In m")][SerializeField] float xMinClampRange = -25.0f, xMaxClampRange = 25.0f;
    [Tooltip("In m")][SerializeField] float yMinClampRange = -11.0f, yMaxClampRange = 11.0f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 3.5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;

    float horizontalThrowInput, verticalThrowInput;
    bool isControlEnabled = true;

    //Updates Player Input Controls Per Frame
    void Update() {
        if (isControlEnabled) {
        TranslationMovementControls();
        RotationMovementControls();
        }
    }

    //Called by string reference from CollisionHandler script
    void OnPlayerDeath() {
        print("Controls frozen");
        isControlEnabled = false;
    }

    //MOVEMENT CONTROLS:

    //Screen Movement
    private void TranslationMovementControls() {
        horizontalThrowInput = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalThrowInput = CrossPlatformInputManager.GetAxis("Vertical");

        //xPosition movement clamped
        float xOffset = horizontalThrowInput * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float xPosClamped = Mathf.Clamp(rawXPos, xMinClampRange, xMaxClampRange);

        //yPosition movement clamped
        float yOffset = verticalThrowInput * controlSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float yPosClamped = Mathf.Clamp(rawYPos, yMinClampRange, yMaxClampRange);

        //Change only x horizontal position and leave y and z alone
        transform.localPosition = new Vector3(xPosClamped, transform.localPosition.y, transform.localPosition.z);
        
        //Change only y horizontal position and leave x and z alone
        transform.localPosition = new Vector3(transform.localPosition.x, yPosClamped, transform.localPosition.z);
        
        //2019 unity site reccs
        //transform.Translate(new Vector3(horizontalThrowInput, verticalThrowInput, 0) * xcontrolSpeed * Time.deltaTime);
    }
    //Rotation Movement
    private void RotationMovementControls() {
        //Pitch calculations refactored
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrowInput * controlPitchFactor;
        
        //Pitch, yaw, and roll calculations
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrowInput * controlRollFactor;

        //Rotating the ship based on calculations
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
