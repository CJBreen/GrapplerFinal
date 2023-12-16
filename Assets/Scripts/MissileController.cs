using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{   
    public float maxSpeed = 5f;
    public float acceleration = 1f;
    public LayerMask wallsDef;
    public float maxDistance = 200f;

    private Transform playerPos;
    private GameObject player;
    private Rigidbody missileRB;
    private Quaternion playerTracking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObject");
        playerPos = player.transform; 
        missileRB = GetComponent<Rigidbody>();
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
        }
    }
}
