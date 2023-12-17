using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Vector3 array that stores all the possible spawn locations for the enemy
    private Vector3[] spawnLocations = {new Vector3(177f, 0f,-165f),
                                        new Vector3(160f, 0f, -143f),
                                        new Vector3(45f, 0f, -34f),
                                        new Vector3(94f, 0f, -91f),
                                        new Vector3(9f, 0f, -160f)
                                        };

    // Public array that stores all the possible enemies that can spawn
    public GameObject[] enemies;
    // Array that stores all the enemies that are currently spawned in
    private GameObject[] numOfEnemies;
    // A private int used to store a random index position of the spawnLocations array
    private int spawnPos;
    public AudioClip spawnClip;

    void FixedUpdate()
    {
        // Getting all enemies and storing them into an array
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        // If there are no enemies on the map, spawn some enemies
        if (numOfEnemies.Length == 0) {
            // Generate a random index position for the spawnLocations array
            spawnPos = Random.Range(0, spawnLocations.Length);
            // Spawn a random enemy at the spawn position with a rotation of 0,0,0,0
            Instantiate(enemies[Random.Range(0, enemies.Length)],
            spawnLocations[spawnPos],
            new Quaternion(0 ,0, 0, 0)
            );
            // Play the enemy spawn audio
            AudioSource.PlayClipAtPoint(spawnClip, spawnLocations[spawnPos]);
            
        }
    }
}
