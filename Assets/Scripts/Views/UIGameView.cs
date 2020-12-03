using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;

/// <summary>
/// Game view with events for buttons and showing data.
/// </summary>
public class UIGameView : UIView
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI healthLabel;
    [SerializeField] private TextMeshProUGUI wavesCounterLabel;

    // Event called when Menu Button is clicked.
    public UnityAction OnMenuClicked;

    public void UpdateScore(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void UpdateHealth(int health)
    {
        healthLabel.text = health.ToString();
    }

    public void UpdateWavesCounter(int waveCounter)
    {
        wavesCounterLabel.text = waveCounter.ToString();
    }

    /// <summary>
    /// Method called by Menu Button.
    /// </summary>
    public void MenuClicked()
    {
        OnMenuClicked?.Invoke();
    }
}