using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller responsible for game over phase.
/// </summary>
public class GameOverController : SubController<UIGameOverRoot>
{
    public override void EngageController()
    {
        // Attaching UI events.
        ui.GameOverView.OnReplayClicked += ReplayGame;
        ui.GameOverView.OnMenuClicked += GoToMenu;

        // Showing game data in UI.
        int lastHighscore = AddressableDataManager.Instance.GetLastHighscore();
        ui.GameOverView.ShowScore(lastHighscore);

        base.EngageController();
    }

    public override void DisengageController()
    {
        base.DisengageController();

        // Detaching UI events.
        ui.GameOverView.OnMenuClicked -= GoToMenu;
        ui.GameOverView.OnReplayClicked -= ReplayGame;
    }

    /// <summary>
    /// Handling UI Replay Button Click.
    /// </summary>
    private void ReplayGame()
    {
        // Changing controller to Game Controller.
        root.ChangeController(RootController.ControllerTypeEnum.Game);
    }

    /// <summary>
    /// Handling UI Menu Button Click.
    /// </summary>
    private void GoToMenu()
    {
        // Changing controller to Menu Controller.
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
        SceneManager.LoadScene("Start Menu");
    }
}