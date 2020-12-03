using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableDataManager : PersistentLazySingleton<AddressableDataManager>
{
    private void OnEnable()
    {
        ES3.DeleteFile("Highscores");
        ES3.Save("Highscores", new Dictionary<string, int>());
    }

    public void PersistScore(int score)
    {
        // Persist last highscore to get retrieved in GameOver Screen
        ES3.Save("LastHighscore", score);

        // Load the Highscores list
        Dictionary<string, int> highscores =
            ES3.Load<Dictionary<string, int>>("Highscores");
        // Add the last Score Entry into the crypted DB
        highscores.Add(DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss"), score);
        ES3.Save("Highscores", highscores);
    }

    public int GetLastHighscore()
    {
        return ES3.Load<int>("LastHighscore");
    }

    public async Task InitAssets<T>(string label, List<T> createdObjs, Transform parent)
         where T : UnityEngine.Object
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var location in locations)
        {
            createdObjs.Add(await Addressables.InstantiateAsync(location, parent).Task as T);
        }
    }
}