using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Vector3 array that stores all the possible spawn locations for the enemy
    private static Vector3[] spawnLocations = {new Vector3(177f, 0f,-165f),
                                        new Vector3(160f, 0f, -143f),
                                        new Vector3(45f, 0f, -34f),
                                        new Vector3(94f, 0f, -91f),
                                        new Vector3(9f, 0f, -160f),
                                        new Vector3(130f, 0f, -26f),
                                        new Vector3(95f, 0f, -30f),
                                        new Vector3(54f, 0f, -38f),
                                        new Vector3(29f, 0f, -55f),
                                        new Vector3(22f, 0f, -94f),
                                        new Vector3(22f, 0f, -148f),
                                        new Vector3(49f, 0f, -166f)
                                        };
    private int[] emptySpawnLoc = new int[spawnLocations.Length];
    // Public array that stores all the possible enemies that can spawn
    public GameObject[] enemies;
    // Array that stores all the enemies that are currently spawned in
    private GameObject[] numOfEnemies;
    // A private int used to store a random index position of the spawnLocations array
    
    public AudioClip spawnClip;
    public int enemyDifficulty = 1;

    void FixedUpdate()
    {
        spawnEnemies();
    }

    // Generates a random spawn location and checks if something has already spawned there
    private int generateSpawnPos() {
        int spawnIndex = Random.Range(0, spawnLocations.Length);

        // Checks if the spawn index is already being used, if not returns it, if it is being used, calls this function recursively again
        if (!System.Array.Exists(emptySpawnLoc, element => element == spawnIndex)) {
            emptySpawnLoc[spawnIndex] = spawnIndex;
            return spawnIndex;
        }
        else {
            return generateSpawnPos();
        }
    }
    private void spawnEnemies() {
        // Getting all enemies and storing them into an array
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        // If there are no enemies on the map, spawn some enemies
        if (numOfEnemies.Length == 0) {
            emptySpawnLoc = new int[spawnLocations.Length]; // Reseting the array that prevents multiple turrets from spawning in the same location
            enemyDifficulty++;  // Adding the num of enemies that are spawning
            for (int iter = 0; iter < enemyDifficulty; iter++) {
                // Generate a random index position for the spawnLocations array
                int spawnPos = generateSpawnPos();

                // Spawn a random enemy at the spawn position with a rotation of 0,0,0,0
                Instantiate(enemies[Random.Range(0, enemies.Length)],
                            spawnLocations[spawnPos],
                            new Quaternion(0 ,0, 0, 0));

                // Play the enemy spawn audio
                AudioSource.PlayClipAtPoint(spawnClip, spawnLocations[spawnPos]);
            }
        }
    }
}
