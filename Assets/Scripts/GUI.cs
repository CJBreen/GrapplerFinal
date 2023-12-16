using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{
    private TextMeshProUGUI playerSpeed;
    private Rigidbody playerRb;
    private Canvas hudMenu;
    private TextMeshProUGUI enemiesRemaining;
    private GameObject[] numOfEnemies;

    public bool isEsc;

    void Start()
    {
        playerSpeed = GameObject.Find("PlayerSpeed").GetComponent<TextMeshProUGUI>();
        hudMenu = GameObject.Find("HUD").GetComponent<Canvas>();
        enemiesRemaining = GameObject.Find("Enemies").GetComponent<TextMeshProUGUI>();
        playerRb = GameObject.Find("PlayerObject").GetComponent<Rigidbody>();
    }

    void Update()
    {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        playerSpeed.text = "Speed: "+Mathf.RoundToInt(Mathf.Abs(playerRb.velocity.magnitude)) + " m/s";
        enemiesRemaining.text = "Enemies Remaining:" + numOfEnemies.Length;
        // playerSpeed.text = "Speed: "+Mathf.RoundToInt(Mathf.Abs(playerRb.velocity.magnitude)*0.2f) + " b/s";    // Bananas Per Second
    }
  
}
