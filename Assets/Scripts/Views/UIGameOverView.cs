using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Game over view with events for buttons and showing data.
/// </summary>
public class UIGameOverView : UIView
{
    // Reference to score label.
    [SerializeField]
    private TextMeshProUGUI scoreLabel;

    // Event called when Replay Button is clicked.
    public UnityAction OnReplayClicked;

    // Event called when Menu Button is clicked.
    public UnityAction OnMenuClicked;

    /// <summary>
    /// Method called by Replay Button.
    /// </summary>
    public void ReplayClick()
    {
        OnReplayClicked?.Invoke();
    }

    /// <summary>
    /// Method called by Menu Button.
    /// </summary>
    public void MenuClicked()
    {
        OnMenuClicked?.Invoke();
    }

    /// <summary>
    /// Method used to show game data in UI.
    /// </summary>
    /// <param name="score">Game data.</param>
    public void ShowScore(int score)
    {
        scoreLabel.text = score.ToString();
    }
}