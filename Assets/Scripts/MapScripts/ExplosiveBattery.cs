using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBattery : MonoBehaviour {
    public GameObject Battery;
    public GameObject explosionEffect;
    public GameObject sparkEffect;
    public AudioSource explosionSound;
    public AudioSource zapSound;

    [SerializeField]
    private float batteryHealth = 50f;
    private float currentBatteryHealth;
    private readonly float damageTime = 0.05f; // amount of time before player gets electricuted again
    private float damageTimer;
    private float targetDamage = 50f;
    private readonly float resetTime = 20f;
    private float currentResetTimer;
    private bool played = false;

    private void Awake() {
        this.GetComponent<SphereCollider>().enabled = false;
    }

    public void Electricute() {

        if (!played) { 
            Instantiate(explosionEffect, transform.position, transform.rotation);
            played = true;
        }

    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) {
            Debug.Log("Player enters battery range");
            damageTimer = damageTime;
        }
    }

    public void OnTriggerStay(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null || other.gameObject.GetComponent<PlayerMovement_Grappler>() != null) { // Check if player

            if (damageTimer <= 0) {
                Debug.Log("Player takes damage");
                other.gameObject.GetComponent<IDamageable>()?.TakeDamage(1, this);
                zapSound.Play();
                damageTimer = damageTime; // Reset Timer
            }

            else {
                damageTimer -= Time.deltaTime;
            }

        }
    }

    IEnumerator SlowDamage(Collider other) {
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(1, this);
        zapSound.Play();
        yield return new WaitForSeconds(1);
    }

    private void Update() {
        this.GetComponent<SphereCollider>().enabled = true;
        Electricute();

    }

}