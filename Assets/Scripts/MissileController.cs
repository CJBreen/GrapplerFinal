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
        explosion.Pause();
        flameTrail.Play();
        player = GameObject.Find("PlayerObject");
        playerRb = player.GetComponent<Rigidbody>();
        playerPos = player.transform; 
        missileRB = GetComponent<Rigidbody>();
        deathScreen = GameObject.Find("DeathScreen").GetComponent<deathScreen>();
    }

    // Update is called once per frame
    void Update(){
        lookAtPlayer();
        missileRB.AddForce((playerTracking*Vector3.forward) * acceleration);
        speedLimiter();
    }

    private void lookAtPlayer() {
        Vector3 missileRotation = playerPos.position - transform.position;
        playerTracking = Quaternion.LookRotation(missileRotation, Vector3.up);
        transform.rotation = playerTracking;
        if (missileRotation.magnitude > maxDistance) {
            Destroy(this.gameObject);
        }
    }
    private void speedLimiter() {
        if (missileRB.velocity.magnitude > maxSpeed) {
            Vector3 fixedVelocity = missileRB.velocity.normalized * maxSpeed;
            missileRB.velocity = fixedVelocity;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if ((wallsDef.value & (1 << other.transform.gameObject.layer)) > 0) {
            Destroy(this.gameObject);
        } else if (other.gameObject.tag == "Player") {
            flameTrail.Stop();
            explosion.Play();
            AudioSource.PlayClipAtPoint(killClip, player.transform.position);
            Invoke("killPlayer", explosion.main.duration/100);
            Destroy(this.gameObject, explosion.main.duration);
        }
    }
    private void killPlayer() {
        deathScreen.playerDeath();
    }
}
