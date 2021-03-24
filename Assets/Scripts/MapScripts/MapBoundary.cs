using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    private Collider player;
    private bool outOfBounds = false;
    private float setTime = 5f;
    private float timer;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") { // Check if it's tagged target
            outOfBounds = true;
            player = other;
            timer = setTime;
        }
    }
    
    public void OnTriggerExit(Collider other) {
        if (other.tag == "Player") { // Check if it's tagged target
            outOfBounds = false;
            
            timer = setTime; // reset timer

            Debug.Log("Player exits battery range");
        }
    }
    
    private void Update() {
        if (outOfBounds) { // Boundary timer countdown
            timer -= 1 * Time.deltaTime;
            Debug.Log("Current Time: " + timer);
        }

        if (timer <= 0) {
            // Teleport
            if (player != null) {
                GameObject spawnPoint = PlayerSpawnManager.NextSpawn();
                player.transform.position = spawnPoint.transform.position;
                timer = setTime; // Reset boundary
            }
        }
    }
 
}
