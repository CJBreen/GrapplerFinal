using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControllerRocket : MonoBehaviour
{
    public LayerMask wallsDef;
    public float rotationOffset;
    public Rigidbody missile;
    public float shootCooldown = 5f;

    private GameObject player;
    private Transform playerPos;
    private bool missileInAir;
    private bool isSeePlayer;
    private Transform gunTipLoc;
    private bool canShoot;

    public AudioSource audioSource;
    public AudioClip killClip;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the player object, position & rotation
        player = GameObject.Find("PlayerObject");
        playerPos = player.transform;
        // Getting the turrets gun tip position & rotation
        // gunTipLoc = GameObject.Find("Rocket Turret Gun Tip").transform;
        // Gives a 5s cooldown before the turret can shoot so it cant just shoot you in the face when you go to punch it
        Invoke("allowShoot", 5f);
    }

    // Update is called once per frame
    void Update()
    {   
        seePlayer();
        lookAtPlayer();
    }
    private void seePlayer() {
        // Creates a line between the turret and the player which is blocked by any walls, if the line hits the player, returns true
        isSeePlayer = !(Physics.Linecast(transform.position, playerPos.position, wallsDef));
        // If the turret can see the player and the shoot cooldown is over, shoot
        if (isSeePlayer && canShoot) {
            shoot();
        }
    }
    private void lookAtPlayer() {
        // If the turret can see the player, get the relative position of the player & turn towards the player
        if (isSeePlayer) {
            Vector3 turretRotation = playerPos.position - transform.position;
            turretRotation.y = 0;   // Prevents the turret from looking up and down
            transform.rotation = Quaternion.LookRotation(turretRotation, Vector3.up);
            // Rotation offset to compensate for misrotated models
            transform.rotation = transform.rotation * Quaternion.Euler(1, rotationOffset, 1);
        }
    }
    private void shoot() {
        // Creates a missile at the guntip's location and with the guntip's rotation
        Instantiate(missile, transform.position+(Vector3.up*3), transform.rotation);
        // Cooldown to prevent the turret from firing too rapidly
        canShoot = false;
        Invoke("allowShoot", shootCooldown);
    }
    private void allowShoot() {
        canShoot = true;
    }

}
