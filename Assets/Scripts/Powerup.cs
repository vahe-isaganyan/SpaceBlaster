using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Powerup class responsible for defining power-up behavior
public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Log a message indicating that the powerup has started
        Debug.Log("Powerup started");
    }

    // Update is called once per frame
    void Update()
    {
        // Move the powerup downward based on its speed and the time since the last frame
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Check if the powerup has moved beyond wanted y position
        if (transform.position.y < -4.5f)
        {
            // Log a message indicating that the powerup has been destroyed
            Debug.Log("Powerup destroyed");

            // Destroy the powerup GameObject when it goes beyond the specified Y position
            Destroy(this.gameObject);
        }
    }

    // Triggered when the powerup collides with another Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with the player
        if (other.tag == "Player")
        {
            // Get reference to the Player script attached to the collided GameObject
            Player player = other.transform.GetComponent<Player>();

            // Null check to ensure Player script reference is not null
            if (player != null)
            {
                // Log a message indicating that the power-up has been collected by the player
                Debug.Log("Powerup collected by player");

                // Activate the multishot powerup in the player script
                player.MultiShotActive();
            }

            // Destroy the powerup GameObject after it has been collected by the player
            Destroy(this.gameObject);
        }
    }
}
