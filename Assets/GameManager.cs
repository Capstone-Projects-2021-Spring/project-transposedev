using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviourPunCallbacks
{
	public static GameManager Instance;
    GameObject controller;
	int botCount;

	private void Awake()
	{
		Instance = this;
		botCount = (int)PhotonNetwork.CurrentRoom.CustomProperties["bots"];
	}

	private void Start()
	{
		SpawnHazards();
		for (int i = 0; i < botCount; i++)
			SpawnAI();
	}

	void SpawnHazards()
	{
        HazardSpawnPoint[] spawnPoints = SpawnManager.Instance.GetHazardSpawnPoints();

        // Iterate through hazard spawn points
        foreach (HazardSpawnPoint spawnPoint in spawnPoints) {

            if (Random.value < 0.6) {
                PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ExplosiveBarrel"), spawnPoint.transform.position, Quaternion.identity);
            }
            else {
                PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ExplosiveBattery"), spawnPoint.transform.position, Quaternion.identity);
            }
            

        }
        
	}


    IEnumerator RespawnHazard(Vector3 position) {
        yield return new WaitForSeconds(10);
        PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "ExplosiveBarrel"), position, Quaternion.identity);
    }
    

    // used to spawn an AI character...
    void SpawnAI()
    {
        // create an AI controller for the AI character...
        CreateAIController();
    }

    // uses the 'CreateController()' method from the 'PlayerManager.cs' scipt as template for creating AI players...
    void CreateAIController()
    {
        // instantiate AI controller
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "AI"), spawnPoint.position, spawnPoint.rotation);
    }

	public void DestroyHazard(GameObject hazard)
	{
        StartCoroutine(RespawnHazard(hazard.transform.position));
		PhotonNetwork.Destroy(hazard);
	}

	public void DestroyAI(GameObject AI)
	{
		PhotonNetwork.Destroy(AI);
		Invoke("SpawnAI", 3);
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene(0);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}
}
