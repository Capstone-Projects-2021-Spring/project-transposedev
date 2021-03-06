using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other) {
        //Destroy(other.gameObject);
        player.transform.position = spawnPoint.transform.position;
    }

}
