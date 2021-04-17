using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager Instance;

	SpawnPoint[] spawnPoints;
    HazardSpawnPoint[] hazardSpawnPoints; 

	void Awake()
	{
		Instance = this;
		spawnPoints = GetComponentsInChildren<SpawnPoint>();
        hazardSpawnPoints = GetComponentsInChildren<HazardSpawnPoint>();
        
	}

	public Transform GetSpawnPoint()
	{
		return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
	}

    // Return list of hazard spawn points
    public HazardSpawnPoint[] GetHazardSpawnPoints() {
        return hazardSpawnPoints;
    }
}
