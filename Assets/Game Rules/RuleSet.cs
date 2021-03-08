using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class RuleSet : MonoBehaviour
{
    [SerializeField]
    private int match_time;
    [SerializeField]
    private float current_time;
    [SerializeField]
    private bool timer_running = false;
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

    void Start()
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

        int mins = Mathf.FloorToInt(current_time / 60);
        int seconds = Mathf.FloorToInt(current_time % 60);

        Debug.Log("Time Remaining: " + mins + ":" + seconds);

        if (current_time <= 0)
            timer_running = false;
    }


}
