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
        player = GameObject.Find("PlayerObject");
        playerPos = player.transform;
        gunTipLoc = GameObject.Find("Rocket Turret Gun Tip").transform;
        Invoke("allowShoot", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        seePlayer();
        lookAtPlayer();
    }
    private void seePlayer() {
        isSeePlayer = !(Physics.Linecast(transform.position, playerPos.position, wallsDef));
        if (isSeePlayer && GameObject.Find("Missile(Clone)") == null && canShoot) {
            shoot();
        }
    }
    private void lookAtPlayer() {
        if (isSeePlayer) {
            Vector3 turretRotation = playerPos.position - transform.position;
            turretRotation.y = 0;
            transform.rotation = Quaternion.LookRotation(turretRotation, Vector3.up);
            transform.rotation = transform.rotation * Quaternion.Euler(1, rotationOffset, 1);
        }
    }
    private void shoot() {
        Instantiate(missile, gunTipLoc.position, gunTipLoc.rotation);
        canShoot = false;
        Invoke("allowShoot", shootCooldown);
    }
    private void allowShoot() {
        canShoot = true;
    }

}
