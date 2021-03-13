using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWeapons : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;

    // Spawn a random weapon from list of weapon prefabs
    private void spawnWeapon() {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("WeaponSpawn"); 

        foreach (GameObject spawn in spawnPoints) {
            Instantiate(weaponPrefabs[UnityEngine.Random.Range(0, weaponPrefabs.Count)], spawn.transform.position, spawn.transform.rotation);
        }
    }

    void Start() {
        spawnWeapon();
    }
}
