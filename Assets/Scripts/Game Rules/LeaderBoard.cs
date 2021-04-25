using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Text lb_text;

	private void Awake()
	{

    }

	void Start()
    {
        //Hashtable hash;
        //foreach (Player p in PhotonNetwork.PlayerList)
        //{
        //    hash = p.CustomProperties;

        //    hash.Remove("kills");
        //    hash.Remove("deaths");

        //    p.SetCustomProperties(hash);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        string kd = "";
        lb_text.text = "";

        try
		{
            foreach (PlayerManager p in FindObjectsOfType<PlayerManager>())
            {
                Debug.Log("PlayerList = " + PhotonNetwork.PlayerList);

                kd = p.PV.Owner.NickName + ": " + p.KillCount() + " / " + p.DeathCount();

                if (p.PV.Owner.Equals(PhotonNetwork.LocalPlayer))
                {
                    kd += " (Me)";
                }
                kd += "\n";

                lb_text.text += kd;
            }
            foreach (AIScript ai in FindObjectsOfType<AIScript>())
            {
                kd = ai.GetId() + ": " + ai.GetKills() + " / " + ai.GetDeaths();
                kd += "\n";

                lb_text.text += kd;
            }

            if (FindObjectOfType<RuleSet>().GameOver())
            {
                kd = "The Winner is: " + DeclareWinner().ToString() + "!!!";
                lb_text.text = kd;
            }
        } 
        catch(Exception e)
		{

		}
    }


    public Player DeclareWinner()
    {
        Player winner = PhotonNetwork.PlayerList[0];

        foreach(Player p in PhotonNetwork.PlayerList)
        {
            if ((int.Parse(p.CustomProperties["kills"].ToString()) - int.Parse(p.CustomProperties["deaths"].ToString())) >= (int.Parse(winner.CustomProperties["kills"].ToString()) - int.Parse(winner.CustomProperties["deaths"].ToString())))
                winner = p;
        }

        return winner;
    }
}
