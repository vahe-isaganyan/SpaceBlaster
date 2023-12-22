using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy class responsible for defining enemy behavior
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Animator _anim;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to animator component
        _anim = GetComponent<Animator>();

        // Check for null reference to Animator
        if (_anim == null)
        {
            Debug.LogError("Animator is null");
        }

        // Get reference to the player script attached to the player game object
        _player = GameObject.Find("Player").GetComponent<Player>();

        // Check for null reference to Player script
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy downward based on speed and time since the last frame
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Log enemy position for debugging
        Debug.Log("Enemy Position: " + transform.position);

        // Check if the enemy has moved beyond set y position
        if (transform.position.y < -5f)
        {
            // Reposition the enemy at a random X position above the screen
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);

            // Log that the enemy has been repositioned for debugging purposes
            Debug.Log("Enemy Repositioned");
        }
    }

    // Triggered when the enemy collides with another Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision 
        if (other.tag == "Player")
        {
            // Get reference to the Player script attached to the collided GameObject
            Player player = other.transform.GetComponent<Player>();

            // Check for null reference to the Player script
            if (player != null)
            {
                // Damage the player
                player.Damage();
            }

            // Trigger the "OnEnemyDestruction" animation
            _anim.SetTrigger("OnEnemyDestruction");

            // Stop the enemy movement so that it can't kil the player before exploding
            _speed = 0;

            // Log that the enemy has been destroyed by the player for debugging 
            Debug.Log("Enemy Destroyed by Player");

            // Destroy the enemy GameObject after a delay
            Destroy(this.gameObject, 1.10f);
        }

        // Check if the collision is with a laser
        if (other.tag == "Laser")
        {
            // Trigger the "OnEnemyDestruction" animation for cool explosion
            _anim.SetTrigger("OnEnemyDestruction");

            // Destroy the collided laser GameObject
            Destroy(other.gameObject);

            //  increase the players score if not null
            {
                _player.PlusScore();
            }

            // Stop the enemys movement
            _speed = 0;

            // Log that the enemy has been destroyed by a laser for debugging purposes
            Debug.Log("Enemy Destroyed by Laser");

            // Destroy the enemy GameObject after a delay
            Destroy(this.gameObject, 1.10f);
        }
    }
}
