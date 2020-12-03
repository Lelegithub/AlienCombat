using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Menu view with events for buttons.
/// </summary>
public class UIMenuView : UIView
{
    // Event called when Play Button is clicked.
    public UnityAction OnPlayClicked;

    /// <summary>
    /// Method called by Play Button.
    /// </summary>
    public void PlayClicked()
    {
        OnPlayClicked?.Invoke();
    }

    // Event called when Quit Button is clicked.
    public UnityAction OnQuitClicked;

    /// <summary>
    /// Method called by Quit Button.
    /// </summary>
    public void QuitClicked()
    {
        OnQuitClicked?.Invoke();
    }
}
