using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IDamageable {
    public GameObject Barrel;
    public GameObject explosionEffect;
    public AudioSource explosionSound;

    [SerializeField]
    private float explosionRange = 8;
    private float targetDamage = 50f;
    private float barrelHealth = 50f;
    private float currentBarrelHealth;
    private float respawnTime = 20f;
    private float currentRespawnTimer;
    private bool played = false;

    
    private void Awake() {
        currentBarrelHealth = barrelHealth;
        currentRespawnTimer = respawnTime;
    }
    

    public void TakeDamage(float damage) {
        currentBarrelHealth -= damage;
        Debug.Log("Ouch. Barrel is hurt. Health:" +  currentBarrelHealth);
    }

    public void Explode() {

        if (!played) {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            explosionSound.Play();
            played = true;

            Collider[] targets = Physics.OverlapSphere(transform.position, explosionRange);
            foreach (Collider target in targets) {
                if (target.gameObject.GetComponent<PlayerMovement>() != null) { // Check it's a player
                    target.GetComponent<Collider>().gameObject.GetComponent<IDamageable>()?.TakeDamage(targetDamage); // Damage the player
                    Debug.Log("Damaged Player!");
                }
            }
        }
        
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

    }

    private void Update() {
        if (currentBarrelHealth <= 0) {
            Explode();
            Respawn();
        }
    }

    private void Respawn() {

        // Set timer
        if (currentRespawnTimer <= 0) {
            currentBarrelHealth = barrelHealth;
            played = false;
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;

            currentRespawnTimer = respawnTime; // Reset Timer
        }

        else { // Countdown
            currentRespawnTimer -= Time.deltaTime;
        }
    }


    // Outline of explosion range
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
