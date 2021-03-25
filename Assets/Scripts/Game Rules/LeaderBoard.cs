using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private Text lb_text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string kd;
        lb_text.text = "";

        if (FindObjectOfType<RuleSet>() != null && !FindObjectOfType<RuleSet>().GameOver())
        {
            
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                kd = p.ToString() + ": " + p.CustomProperties["kills"].ToString() + " / " + p.CustomProperties["deaths"];

                if (p.Equals(PhotonNetwork.LocalPlayer))
                {
                    kd += " (Me)";
                }
                kd += "\n";

                lb_text.text += kd;
            }
        }
        else if(FindObjectOfType<RuleSet>().GameOver())
        {
            kd = "The Winner is: " + DeclareWinner().ToString() + "!!!";
            lb_text.text = kd;
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
