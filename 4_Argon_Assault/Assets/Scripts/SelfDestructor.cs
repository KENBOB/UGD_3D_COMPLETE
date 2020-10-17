using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float DestructionDelay = 5f;

    //Destroy the created junk game objects    
    void Start()
    {
        Destroy(gameObject, DestructionDelay);
    }
}
