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

    // player properties
    Hashtable hash = new Hashtable();

    void Awake()
	{
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            hash.Add("itemIndex", 0);
            hash.Add("deaths", 0);
            hash.Add("kills", 0);
            hash.Add("class", "PlayerController"); // default player controller class
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
		{
            // Instantiate player controller
            CreateController("PlayerController");
        }
    }

	void CreateController(string className)
	{
        // Instantiate player controller
        Transform spawnPoint = SpawnManager.Instance.GetSpawnPoint();
        controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", className), spawnPoint.position, spawnPoint.rotation, 0, new object[] { PV.ViewID });
    }

	public void Die(Player shooter)
	{
        Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;
        UpdateDeaths(deaths + 1);
        if (!PhotonNetwork.LocalPlayer.Equals(shooter))
            UpdateKills(shooter);
        PhotonNetwork.Destroy(controller);
		CreateController((string)hash["class"]);
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
        if (changedProps.Count < 3)
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
