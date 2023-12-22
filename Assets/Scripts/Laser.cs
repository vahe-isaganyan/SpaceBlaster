using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Laser class responsible for laser behavior
public class Laser : MonoBehaviour
{
    // SerializedField allows private variable _laserSpeed to be accessed from the unity inspector
    [SerializeField]
    private float _laserSpeed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        // Log the current position of the laser for debugging purposes
        Debug.Log("Laser Position: " + transform.position);

        // Move the laser upward based on its speed and time since last frame
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        // Check if laser has moved beyond Y position so that we can destroy it and free up resources for performance
        if (transform.position.y > 8f)
        {
            // Log that the laser has been destroyed
            Debug.Log("Laser Destroyed");

            // Destroy the laser for performance reasons
            Destroy(this.gameObject);
        }
    }
}
