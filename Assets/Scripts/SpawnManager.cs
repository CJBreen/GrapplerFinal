using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3[] spawnLocations = {new Vector3(177f, 0f,-165f),
                                        new Vector3(129f, 0f, -143f),
                                        new Vector3(45f, 0f, -34f),
                                        new Vector3(94f, 0f, -91f),
                                        new Vector3(9f, 0f, -160f)
                                        };

    public GameObject[] enemies;
    private GameObject[] numOfEnemies;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("Number of enemies"+numOfEnemies.Length);
        if (numOfEnemies.Length == 0) {
            Instantiate(enemies[Random.Range(0, enemies.Length)],
            spawnLocations[Random.Range(0, spawnLocations.Length)],
            new Quaternion(0 ,0, 0, 0)
            );
        }
    }
}
