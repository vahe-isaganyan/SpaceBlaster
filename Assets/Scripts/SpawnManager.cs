using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SpawnManager class responsible for managing enemy and power-up spawning
public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _multiShotPrefab;

    private bool _stopSpawningEnemies = false;
    private bool _stopSpawningMultishot = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start coroutine for spawning regular enemies
        StartCoroutine(SpawnOpponentRoutine());

        // Start coroutine for spawning multi-shot power-ups
        StartCoroutine(SpawnMultishotRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Log an update message for debugging purposes
        Debug.Log("SpawnManager Update");
    }

    // Coroutine to spawn regular enemies
    IEnumerator SpawnOpponentRoutine()
    {
        while (!_stopSpawningEnemies)
        {
            // Generate random position within a specified range
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            // Instantiatenew enemy at position
            GameObject newEnemy = Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity);

            // Set the enemys parent to the enemy container
            newEnemy.transform.parent = _enemyContainer.transform;

            // Log the spawn position of the enemy for debugging
            Debug.Log("Spawned enemy at: " + postToSpawn);

            // Wait for a specified duration before spawning the next enemy
            yield return new WaitForSeconds(5.0f);
        }
    }

    // Coroutine to spawn multishot powerups
    IEnumerator SpawnMultishotRoutine()
    {
        while (!_stopSpawningMultishot)
        {
            // Generate a random position within wanted range
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            // Log the spawn position of the multishot powerup for debugging purposes
            Debug.Log("Spawned multishot powerup at: " + postToSpawn);

            // Instantiate a new multishot powerup at the generated position
            Instantiate(_multiShotPrefab, postToSpawn, Quaternion.identity);

            // Log a message indicating that a multishot powerup has been spawned
            Debug.Log("Spawned multishot powerup");

            // Wait for a random duration before spawning the next multishot powerup
            yield return new WaitForSeconds(Random.Range(15, 18));
        }
    }

    // Called when the player dies to stop spawning enemies and powerups
    public void OnPlayerDeath()
    {
        _stopSpawningEnemies = true;
        _stopSpawningMultishot = true;

        // Log a message indicating that spawning has stopped cause the player has died
        Debug.Log("Spawning stopped due to player death");
    }
}
