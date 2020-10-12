using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^1")][SerializeField] float OffsetSpeed = 40f;
    [Tooltip("In m")][SerializeField] float xMinClampRange = -25.0f, xMaxClampRange = 25.0f;
    [Tooltip("In m")][SerializeField] float yMinClampRange = -11.0f, yMaxClampRange = 11.0f;

    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 3.5f;
    [SerializeField] float controlRollFactor = -20f;
    float horizontalThrowInput, verticalThrowInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TranslationMovementControls();
        RotationMovementControls();
    }

    //Movement Controls
    //Screen Movement
    private void TranslationMovementControls(){
        horizontalThrowInput = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalThrowInput = CrossPlatformInputManager.GetAxis("Vertical");

        //xPosition movement clamped
        float xOffset = horizontalThrowInput * OffsetSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float xPosClamped = Mathf.Clamp(rawXPos, xMinClampRange, xMaxClampRange);

        //yPosition movement clamped
        float yOffset = verticalThrowInput * OffsetSpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float yPosClamped = Mathf.Clamp(rawYPos, yMinClampRange, yMaxClampRange);

        //change only x horizontal position and leave y and z alone
        transform.localPosition = new Vector3(xPosClamped, transform.localPosition.y, transform.localPosition.z);
        //change only y horizontal position and leave x and z alone
        transform.localPosition = new Vector3(transform.localPosition.x, yPosClamped, transform.localPosition.z);
        
        //2019 unity site reccs
        //transform.Translate(new Vector3(horizontalThrowInput, verticalThrowInput, 0) * xOffsetSpeed * Time.deltaTime);
    }
    //Rotation Movement
    private void RotationMovementControls(){
        //pitch calculations refactored
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrowInput * controlPitchFactor;
        
        //pitch, yaw, and roll calculations
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrowInput * controlRollFactor;
        //rotating the ship based on calculations
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
