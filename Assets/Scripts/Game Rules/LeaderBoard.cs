using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LeaderBoard : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            string kd = p.ToString() + ": " + p.CustomProperties["kills"].ToString() + " / " + p.CustomProperties["deaths"];
            Debug.Log(kd);
        }
    }


    public Player DeclareWinner()
    {
        Player winner = PhotonNetwork.PlayerList[0];

        foreach(Player p in PhotonNetwork.PlayerList)
        {
            if (int.Parse(p.CustomProperties["kills"].ToString()) >= int.Parse(winner.CustomProperties["kills"].ToString()))
                winner = p;
        }

        return winner;
    }
}
