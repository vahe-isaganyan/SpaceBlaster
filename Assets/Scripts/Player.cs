using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class responsible for defining player behavior
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _multiShotPrefab;

    [SerializeField]
    private float _fireRate = 0.3f;
    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;

    public Vector3 Vector3 { get; private set; }

    [SerializeField] // Simulate for testing
    private bool _isMultiShotActive = false;

    private int _score;

    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position for player
        transform.position = new Vector3(0, 0, 0);

        // Get references to SpawnManager and UIManager
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        // Check for null references
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handl ing of player behavior
        CalculateMovement();
        ShootLaser();

        // Log player position for debugging purposes
        Debug.Log("Player Position: " + transform.position);
    }

    // Function to handle player movement
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Translate the player based on input and speed
        transform.Translate(_speed * horizontalInput * Time.deltaTime * Vector3.right);
        transform.Translate(_speed * Time.deltaTime * verticalInput * Vector3.up);

        // Log input for debugging purposes
        Debug.Log("Horizontal Input: " + horizontalInput + ", Vertical Input: " + verticalInput);

        // Clamp player position to screen boundaries  and allow to move across
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    // Function to handle shooting lasers
    void ShootLaser()
    {
        Vector3 laserSpawn = transform.position;
        laserSpawn.y += 0.75f;

        // Check for the space key press and also fire rate cooldown to not allow infinite firing
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            // Check if multi-shot power-up is active
            if (_isMultiShotActive)
            {
                // Define spread angle and starting angle for multi-shot
                float spreadAngle = 10f; // Spread angle between lasers
                float startingAngle = -2 * spreadAngle; // Starting angle for the first laser

                // Spawn multiple lasers with different rotations for the multishot for variety
                for (int i = 0; i < 4; i++)
                {
                    // Calculate each laser rotation for the multi shot
                    Quaternion rotation = Quaternion.Euler(0, 0, startingAngle + i * spreadAngle);
                    Instantiate(_multiShotPrefab, laserSpawn, rotation);
                }
            }
            else
            {
                // Spawn single laser
                Instantiate(_laserPrefab, laserSpawn, Quaternion.identity);

                // Log that a laser has been fired for debugging purposes
                Debug.Log("Laser Fired");
            }
        }
    }

    // Function to handle player damage
    public void Damage()
    {
        _lives--;

        // Log remaining lives for debugging purposes for future enhancements of additional lives
        Debug.Log("Lives Remaining: " + _lives);

        // Check if player has no remaining lives
        if (_lives < 1)
        {
            // destroy player object after game is over
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    // Function to activate multishot powerup
    public void MultiShotActive()
    {
        _isMultiShotActive = true;
        StartCoroutine(MultiShotOff());
    }

    // Coroutine to deactivate multishot powerup 
    IEnumerator MultiShotOff()
    {
        yield return new WaitForSeconds(7.0f);
        _isMultiShotActive = false;
    }

    // Function to increase the player score for when they destroy an enemy
    public void PlusScore()
    {
        _score += 5;
        _uiManager.UpdateScore(_score);
    }
}
