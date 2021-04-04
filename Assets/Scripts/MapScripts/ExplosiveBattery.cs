using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBattery : MonoBehaviour, IDamageable {
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
        currentBatteryHealth = batteryHealth;
        currentResetTimer = resetTime;
        this.GetComponent<SphereCollider>().enabled = false;
    }

    public void TakeDamage(float damage) {
        currentBatteryHealth -= damage;
        Debug.Log("Ouch. Battery is hurt. Health:" + currentBatteryHealth);
    }

    public void Electricute() {
        
        if (!played) { // Effects play once
            explosionSound.Play();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            played = true;
        }
        ResetBattery();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) {
            Debug.Log("Player enters battery range");
            damageTimer = damageTime;
        }
    }

    public void OnTriggerStay(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) { // Check if player

            if (damageTimer <= 0 ) {
                Debug.Log("Player takes damage");
                other.gameObject.GetComponent<IDamageable>()?.TakeDamage(1);
                zapSound.Play();
                damageTimer = damageTime; // Reset Timer
            }

            else {
                damageTimer -= Time.deltaTime;
            }
            
        }
    }

    private void ResetBattery() {
        // Set timer
        if (currentResetTimer <= 0) {
            currentBatteryHealth = batteryHealth;
            played = false;
            currentResetTimer = resetTime; // Reset Timer
            this.GetComponent<SphereCollider>().enabled = false;
        }

        else { // Countdown
            currentResetTimer -= Time.deltaTime;
        }
    }

    IEnumerator SlowDamage(Collider other) {
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(1);
        zapSound.Play();
        yield return new WaitForSeconds(1);
    }

    private void Update() {
        if (currentBatteryHealth <= 0) {
            this.GetComponent<SphereCollider>().enabled = true;
            Electricute();
        }
        
    }

}