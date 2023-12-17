using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControllerSniper : MonoBehaviour
{
    public LayerMask wallsDef;
    public float turretViewHeight;
    public float shootTimer;
    public float rotationOffset;
    public ParticleSystem firingSFX;
    
    private Transform playerPos;
    private GameObject player;
    private LineRenderer lineRender;
    private bool isSeePlayer;
    private Transform gunTip;
    private float playerHeight;
    private float playerHeight_Offset = 4f;
    private bool isShooting;
    private float currentShootingTimer;

    public AudioSource audioSource;
    public AudioClip killClip;
    
    deathScreen deathScreen;
    
    void Start() {
        // Getting the player object and position and rotation
        player = GameObject.Find("PlayerObject");
        playerPos = player.transform;
        // Getting the player height from the variable stored in playercontroller
        playerHeight = player.GetComponent<PlayerController>().playerHeight;
        // Getting the linerender component of the turret
        lineRender = GetComponent<LineRenderer>();
        // Getting the death screen
        deathScreen = GameObject.Find("DeathScreen").GetComponent<deathScreen>();
        // Getting the position and rotation of the gun tip, which is just the very end of the gun
        gunTip = GameObject.Find("Gun Tip").transform;
        // Setting the countdown timer to what is stored in the public variable (the variable is counted down before the gun is fired)
        currentShootingTimer = shootTimer;
        // Stops the shooting effect from playing
        firingSFX.Stop();
        
    }
    // Using FixedUpdate for performance reasons
    void FixedUpdate() {
        // Calling all of the functions that the turret performs
        seePlayer();    // The script to check if the turret can see the player
        lookAtPlayer(); // The script to make the turret track the player
        drawLine();     // Draws a line from the turret to the player to let the player know its being targeted
    }

    public void killPlayer()
    {
        AudioSource.PlayClipAtPoint(killClip, player.transform.position);
        deathScreen.playerDeath();
    }


    private void seePlayer() {
        // Draws a line from the turret to the player, the line is blocked by any walls & if the line can hit the player, returns true
        isSeePlayer = !(Physics.Linecast(transform.position, playerPos.position, wallsDef));
        // if the turret sees the player and isnt shooting, begin shooting
        if (isSeePlayer) {
            if (!isShooting) {
                isShooting = true;
                InvokeRepeating("shootGun", 0, 1);
            }
        }
        // else stop shooting and reset the countdown timer
        else {
            isShooting = false;
            CancelInvoke("shootGun");
            currentShootingTimer = shootTimer;
        }
    }

    // Method to make the turret look at the player
    private void lookAtPlayer() {
        // If the turret can see the player, look at them
        if (isSeePlayer) {
            // Getting the players relative position to the turret
            Vector3 turretRotation = playerPos.position - transform.position;
            turretRotation.y = 0;   // We dont want or need the turret to rotate vertically in this case
            // Look at the generated position
            transform.rotation = Quaternion.LookRotation(turretRotation, Vector3.up);
            // Offset the rotation to take into account the turret's model being rotated at a different angle
            transform.rotation = transform.rotation * Quaternion.Euler(1, rotationOffset, 1);
        }
    }
    private void drawLine() {
        // If the turret can see the player, draw a line to them
        if (isSeePlayer) {
            lineRender.positionCount = 2;
            lineRender.SetPosition(0, gunTip.position); // First point of the line
            // We want the laser to be aimed at the players feet, not in their face so we offset the height
            lineRender.SetPosition(1, playerPos.position - Vector3.up * (playerHeight-playerHeight_Offset));    // Second point of the line
        }
        else {
            // Deletes the line
            lineRender.positionCount = 0;
        }
    }
    // Method to shoot the gun
    private void shootGun() {
        // If the timer is still not at 0, decrement it and also reduce the line width to notify the player that they're about to get shot
        if (currentShootingTimer > 0) {
            currentShootingTimer -= 1;
            lineRender.widthMultiplier = currentShootingTimer/shootTimer;
        }
        else {
            // Play the gun shooting effect and invoke the kill player script
            firingSFX.Play();
            Invoke("killPlayer", firingSFX.main.duration/100);
        }
        
    }
    
   

}
