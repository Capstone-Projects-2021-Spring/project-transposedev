using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            player = other.transform;
            other.gameObject.GetComponent<PlayerMovement>().Die();

            GameObject spawnPoint = PlayerSpawnManager.NextSpawn(); // Grabs random spawn from list of spawn points in the scene
            player.transform.position = spawnPoint.transform.position;
        }

    }

}
