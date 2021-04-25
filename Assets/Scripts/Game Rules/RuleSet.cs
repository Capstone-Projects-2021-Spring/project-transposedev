using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Timers;
using Photon.Pun;
using Photon.Realtime;
using static EscMenu;

public class RuleSet : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int match_time;
    [SerializeField]
    private float current_time;
    [SerializeField]
    private bool timer_running = false;

    [SerializeField]
    private int targets_destroyed = 0;

    [SerializeField]
    private int max_players;
    [SerializeField]
    private bool insta_gib;
    [SerializeField]
    private bool spawn_pickups;
    [SerializeField]
    private bool cooldowns;
    [SerializeField]
    private List<string> weapons_allowed;

    private bool game_ending = false;

    void Awake()
    {
        SetMatchTime(1);
        StartMatchTimer();
    }

    
    void Update()
    {
        if (timer_running)
            UpdateTimer();

        if(GameOver() && !game_ending)
        {
            game_ending = true;
            StartCoroutine(GameEnd());
        }
    }

    public void SetMatchTime(int mins)
    {
        match_time = mins;
        timer_running = true;
    }

    private void StartMatchTimer()
    {
        current_time = (float)match_time * 60; //convert to seconds
    }

    private void UpdateTimer()
    {
        if (current_time <= 0)
        {
            timer_running = false;
        }
        else
        {
            current_time -= Time.deltaTime;
        }
    }

    public string GetTime()
    {
        string mins = Mathf.FloorToInt(current_time / 60).ToString();
        int seconds = Mathf.FloorToInt(current_time % 60);

        string sec;
        if (seconds < 10)
            sec = "0" + seconds.ToString();
        else
            sec = seconds.ToString();

        if (current_time <= 0)
            return "0:00";

        return mins + ":" + sec;
    }

    public float GetTimeFloat()
    {
        return current_time;
    }

    public void TargetDestroyed()
    {
        if(timer_running)
            targets_destroyed++;
    }

    public int TargetCounter()
    {
        return targets_destroyed;
    }

    public bool GameOver()
    {
        return !timer_running;
    }

    public IEnumerator GameEnd()
    {
        yield return new WaitForSeconds(6);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            RoomManager.Instance.ReturnToRoomMenu();
        }
        Destroy(gameObject);
    }

    public void ExitMap()
    {


    }


}
