﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBattery : MonoBehaviour, IDamageable {
    public GameObject Battery;
    public GameObject explosionEffect;
    public GameObject sparkEffect;
    public AudioSource explosionSound;
    public AudioSource zapSound;

    [SerializeField]
    private float explosionRange;
    private float batteryHealth = 50f;
    private bool inRange =  false;
    private bool destroyed = true;
    private float health = 100f;
    private float damageTime = 3; // amount of time before player gets electricuted again
    private float damageDuration;
    private bool played = false;

    private void Awake() {
        Battery.SetActive(true);
        this.GetComponent<SphereCollider>().enabled = false;
    }

    public void TakeDamage(float damage) {
        batteryHealth -= damage;
        Debug.Log("Ouch. Battery is hurt. Health:" + batteryHealth);
    }

    public void Electricute() {
        
        if (!played) {
            explosionSound.Play();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            played = true;
        }

        if (inRange) { // Damage timer countdown
            zapSound.Play();
            damageDuration -= 1 * Time.deltaTime;
            Debug.Log("Current Time: " + damageDuration);
        }

        if (damageDuration <= 0) { // electricute once countdown ended
            health -= 10;
            damageDuration = damageTime; // Reset damage timer
        }

    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) {
            Debug.Log("Player enters battery range");
            inRange = true;
            damageDuration = damageTime;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) { // Check if it's tagged target
            inRange = false;
            damageDuration = 2; // reset timer
            Debug.Log("Player exits battery range");
            Debug.Log("Current Health: " + health);
        }
    }

    private void Update() {
        if (batteryHealth <= 0) {
            this.GetComponent<SphereCollider>().enabled = true;
            Electricute();
        }
        
    }
}