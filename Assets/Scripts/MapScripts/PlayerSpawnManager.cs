using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpawnManager : MonoBehaviour {

    [Header("Players")]
    [SerializeField] private Transform player;

    // Give player random spawn from set of spawns
    public static GameObject NextSpawn() {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");  //returns GameObject[]
        GameObject spawn = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        return spawn;
    }

    void Start() {
        player.transform.position = NextSpawn().transform.position;
    }

}








