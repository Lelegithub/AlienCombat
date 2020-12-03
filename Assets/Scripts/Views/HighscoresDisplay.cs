using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoresDisplay : MonoBehaviour
{
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private GameObject content;

    // Start is called before the first frame update
    private void OnEnable()
    {
        // Load the Highscores and display them
        Dictionary<string, int> highscores =
            ES3.Load<Dictionary<string, int>>("Highscores");

        foreach (KeyValuePair<string, int> k in highscores)
        {
            Instantiate(
                scoreEntryPrefab,
                content.transform)
                .GetComponentInChildren<TextMeshProUGUI>()
                .text = $"Score:{k.Value}" +
                $"- Date: {k.Key}";
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}