using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    [SerializeField]
    private float setTime = 5f;
    private Collider player;
    private bool outOfBounds = false;
    private float currentTime;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) { // Check if it's tagged target
            outOfBounds = true;
            player = other;
            currentTime = setTime;
        }
    }
    
    public void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<PlayerMovement>() != null) { // Check if it's tagged target
            outOfBounds = false;
            
            currentTime = setTime; // reset timer

            Debug.Log("Player exits battery range");
        }
    }
    
    private void Update() {
        if (outOfBounds) { // Boundary timer countdown
            currentTime -= 1 * Time.deltaTime;
            Debug.Log("Out of Bounds Current Time: " + currentTime);
        }

        if (currentTime <= 0) {
            // Teleport
            if (player != null) {
                GameObject spawnPoint = PlayerSpawnManager.NextSpawn();
                player.transform.position = spawnPoint.transform.position;
                currentTime = setTime; // Reset boundary
            }
        }
    }
 
}
