using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3[] spawnLocations = {new Vector3(10f,-3f,-7.5f),
                                        new Vector3(-33f, -3f, -7.5f),
                                        new Vector3(-56f, -3f, 55.5f),
                                        new Vector3(-114f, -3f, 22f),
                                        new Vector3(-140f, -3f, -2f)
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
