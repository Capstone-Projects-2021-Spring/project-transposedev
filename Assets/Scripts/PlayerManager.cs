using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
	GameObject controller;
	[SerializeField]private int kills = 0;
    [SerializeField]private int deaths = 0;
    private string myClassName;

    // player properties
    Hashtable hash;

    void Awake()
	{
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("itemIndex", 0);
            hash.Add("deaths", 0);
            hash.Add("kills", 0);
            hash.Add("class", "Teleporter"); // default player controller class
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
		{
            // Instantiate player controller
            CreateController("Teleporter");
        }
    }

	void CreateController(string className)
	{
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        switch (className)
		{
            case "Grappler":
                controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerControllerGrappler"), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
                break;
            case "Teleporter":
                controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
                break;
            case "Gravitator":
                controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
                break;
		}
    }



	public void Die(Player shooter, string botId)
	{
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        UpdateDeaths(deaths + 1);

        if(shooter != null && !shooter.Equals(PhotonNetwork.LocalPlayer))
            UpdateKills(shooter);

        if(botId != null)
		{
            GiveBotKill(botId);
		}

        PhotonNetwork.Destroy(controller);
		CreateController((string)hash["class"]);
	}

    private void GiveBotKill(string botId)
	{
        string key = botId + "_kills";
        Hashtable hash = PhotonNetwork.MasterClient.CustomProperties;
        int botkills = (int)hash[key] + 1;
        hash.Remove(key);
        hash.Add(key, botkills);
        PhotonNetwork.MasterClient.SetCustomProperties(hash);
	}

	public void UpdateKills(Player shooter)
	{
        int newKillCount;
        if (PV.IsMine)
        {
            Hashtable hash = shooter.CustomProperties;
            newKillCount = ((int)hash["kills"]) + 1;
            hash.Remove("kills");
            hash.Add("kills", newKillCount);
            shooter.SetCustomProperties(hash);
        }
    }

    public void UpdateKills(int newKillCount)
	{
        kills = newKillCount;
	}

    public void UpdateDeaths(int newDeathCount)
	{
        deaths = newDeathCount;
        if (PV.IsMine)
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Remove("deaths");
            hash.Add("deaths", newDeathCount);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

	public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
        if (changedProps.Count < 4)
            return;

		if (!PV.IsMine && targetPlayer == PV.Owner)
		{
            UpdateDeaths((int)changedProps["deaths"]);
		}

        if (targetPlayer == PV.Owner)
		{
            UpdateKills((int)changedProps["kills"]);
		}
	}

	public int KillCount()
    {
        return kills;
    }

    public int DeathCount()
    {
        return deaths;
    }
}
