using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using static EscMenu;

public class RuleSet : MonoBehaviour
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

    void Awake()
    {
        SetMatchTime(3);
        StartMatchTimer();
    }

    
    void Update()
    {
        if (timer_running)
            UpdateTimer();
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
        current_time -= Time.deltaTime;

        if (current_time <= 0)
        {
            timer_running = false;
            FindObjectOfType<EscMenu>().OnClickQuit();
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


}
