using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billBoard : MonoBehaviour
{
    public Transform cam; // Reference to the main camera

    private void Start()
    {
        cam = Camera.main.transform; // Assign the main camera transform
    }

    private void LateUpdate()
    {
        // Rotate the sprite to face the camera
        transform.LookAt(transform.position + cam.forward);
    }
}