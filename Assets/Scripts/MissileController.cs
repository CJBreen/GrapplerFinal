using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{   
    public float maxSpeed = 5f;
    public float acceleration = 1f;
    public LayerMask wallsDef;
    public float maxDistance = 200f;
    public ParticleSystem explosion;
    public ParticleSystem flameTrail;

    private Transform playerPos;
    private GameObject player;
    private Rigidbody missileRB;
    private Rigidbody playerRb;
    private Quaternion playerTracking;

    deathScreen deathScreen;
    public AudioClip killClip;

    // Start is called before the first frame update
    void Start()
    {
        // Stop explosion effect and start the flame trail effect
        explosion.Pause();
        flameTrail.Play();
        // Get the player position, rotation and rigidbody
        playerRb = GameObject.Find("PlayerObject").GetComponent<Rigidbody>();
        playerPos = GameObject.Find("PlayerObject").transform;

        // Get the missile's rigidbody
        missileRB = GetComponent<Rigidbody>();
        deathScreen = GameObject.Find("DeathScreen").GetComponent<deathScreen>();
    }

    // Update is called once per frame
    void Update(){
        // Make the missle face the player
        lookAtPlayer();
        // Make the missile move in whatever direction its looking in
        missileRB.AddForce((playerTracking*Vector3.forward) * acceleration);
        // Adding a speedlimit to the missile
        speedLimiter();
    }

    private void lookAtPlayer() {
        // Getting the relative position of the player to the missile
        Vector3 missileRotation = playerPos.position - transform.position;
        // Using the midpoint to look at the player
        playerTracking = Quaternion.LookRotation(missileRotation, Vector3.up);
        transform.rotation = playerTracking;

        // If the missile flies too far away, destroy it
        if (missileRotation.magnitude > maxDistance) {
            Destroy(this.gameObject);
        }
    }

    private void speedLimiter() {
        // If the missile's velocity is greater than the max speed, put a hard speed limit on it
        if (missileRB.velocity.magnitude > maxSpeed) {
            Vector3 fixedVelocity = missileRB.velocity.normalized * maxSpeed;
            missileRB.velocity = fixedVelocity;
        }
    }

    private void OnTriggerEnter(Collider other) {
        // If the missile hits the walls or the floor, destroy it
        // Converting from layer to layermask and comparing against the int value of the layermask (1 << layer is a fancy way of converting the layer to the layermask and since other spits out the layer not the layermask, this is required...)
        if ((wallsDef.value & (1 << other.transform.gameObject.layer)) > 0) {
            Destroy(this.gameObject);
        } 
        // If the missile hits the player, create an explosion and kill the player and delete the missile
        else if (other.gameObject.tag == "Player") {
            flameTrail.Stop();
            explosion.Play();
            AudioSource.PlayClipAtPoint(killClip, playerPos.position);
            Invoke("killPlayer", explosion.main.duration/100);
            Destroy(this.gameObject, explosion.main.duration);
        }
    }
    private void killPlayer() {
        deathScreen.playerDeath();
    }
}
