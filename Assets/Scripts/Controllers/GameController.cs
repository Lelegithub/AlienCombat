using UnityEngine;
using Cysharp.Threading.Tasks;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Zenject;

/// <summary>
/// Controller responsible for game phase.
/// </summary>
public class GameController : SubController<UIGameRoot>
{
    private int score = 0;
    private Player player;
    private EnemySpawner enemySpawner;

    [SerializeField] private Transform PlayerSpawnPosition;

    public override void EngageController()
    {
        //Start the Game Coroutine
        GameCoroutine();
    }

    private async UniTask SpawnPlayer()
    {
        // Set The Position for Player to be spawn
        PlayerSpawnPosition = GameObject.FindGameObjectWithTag("PlayerSpawnPosition").transform;

        // Get Player Prefab from Addressable DB
        List<GameObject> _createdObjs = new List<GameObject>();
        await AddressableDataManager.Instance.InitAssets("Player", _createdObjs, PlayerSpawnPosition);
        player = _createdObjs[0].GetComponent<Player>();

        // Attaching UI events.
        player.OnGameOverEvent += FinishGame;
    }

    public override void DisengageController()
    {
        // Detaching UI events.
        if (player != null)
            player.OnGameOverEvent -= FinishGame;

        base.DisengageController();
    }

    async private UniTaskVoid GameCoroutine()
    {
        // Reset GameController dependencies
        score = 0;
        ui.GameView.UpdateScore(0);
        ui.GameView.OnMenuClicked += GoToMenu;
        base.EngageController();
        await UniTask.Delay(1000);
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        enemySpawner.Reset();
        ui.GameView.UpdateWavesCounter(enemySpawner.WaveCounter);

        // Spawn the player
        await SpawnPlayer();

        //Update the UI every half second
        while (true)
        {
            // Displaying current game score & player's health
            ui.GameView.UpdateScore(score);
            ui.GameView.UpdateHealth(player.GetHealth());
            ui.GameView.UpdateWavesCounter(enemySpawner.WaveCounter);
            await UniTask.Delay(500); // Delay HUD refresh for performance
        }
    }

    private void FinishGame()
    {
        // Saving GameData in DataStorage.
        AddressableDataManager.Instance.PersistScore(score);
        // Chaning controller to Game Over Controller
        root.ChangeController(RootController.ControllerTypeEnum.GameOver);
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    private void GoToMenu()
    {
        // Changing controller to Menu Controller.
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
        SceneManager.LoadScene("Start Menu");
    }
}