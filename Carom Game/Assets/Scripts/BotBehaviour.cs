using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    public CircularTimer timer2;
    public bool isBotPlayerActive = false;

    private void Start()
    {
        // Subscribe to the timer's events
        timer2.OnTimerStarted += OnTimer2Started;
        timer2.OnTimerStopped += OnTimer2Stopped;
    }

    private void Update()
    {
    }

    private void OnTimer2Started()
    {
        isBotPlayerActive = true;
        // Start the bot player logic
        StartCoroutine(BotPlayerRoutine());
    }

    private void OnTimer2Stopped()
    {
        isBotPlayerActive = false;
    }

    private IEnumerator BotPlayerRoutine()
    {
        while (isBotPlayerActive)
        {
            yield return new WaitForSeconds(1f);
        }
    }
}
