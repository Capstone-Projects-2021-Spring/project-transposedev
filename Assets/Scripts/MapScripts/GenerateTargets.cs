using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTargets : MonoBehaviour {

    public GameObject target;

    // grab all target spawn points in scene
    private void spawnTargets() {

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("TargetSpawn"); 
        foreach (GameObject spawn in spawnPoints) {
            Instantiate(target, spawn.transform.position, spawn.transform.rotation);
        }
    }

    void Start() {
        spawnTargets();
    }
}
