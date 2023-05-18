using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularTimer : MonoBehaviour
{
    public float totalTime = 10f;
    public float updateInterval = 0.02f;
    private float timeElapsed;
    private bool isRunning;
    private bool isAlternatePosition = false;

    public Image foregroundImage1;
    public Image foregroundImage2;
    private Image currentImage;

    public event Action OnTimerStarted;
    public event Action OnTimerStopped;

    private PrefabManager prefabManager;

    private void Start()
    {
        prefabManager = FindObjectOfType<PrefabManager>();

        currentImage = foregroundImage1;
    }

    public void StartTimer()
    {
        if (!isRunning)
        {
            isRunning = true;
            timeElapsed = 0f;
            SwapCurrentImage();
            InvokeRepeating("UpdateTimerVisuals", 0f, updateInterval);
        }
    }

    public void StopTimer()
    {
        isRunning = false;
        CancelInvoke("UpdateTimerVisuals");
    }

    private void UpdateTimerVisuals()
    {
        timeElapsed += updateInterval;
        float fillAmount = (timeElapsed / totalTime);
        currentImage.fillAmount = fillAmount;

        if (timeElapsed >= totalTime)
        {
            // Timer has finished
            isRunning = false;
            isAlternatePosition = !isAlternatePosition;
            CancelInvoke("UpdateTimerVisuals");
        }
    }

    private void SwapCurrentImage()
    {
        if (isAlternatePosition)
        {
            OnTimerStarted?.Invoke();
            currentImage = foregroundImage2;
            prefabManager.StoreCoinPositions();
        }
        else
        {
            OnTimerStopped?.Invoke();
            currentImage = foregroundImage1;
            prefabManager.blackCoinPositions.Clear();
        }
    }
}
