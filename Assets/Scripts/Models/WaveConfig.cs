using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    
    [SerializeField] int numberOfEnemies = 5;


    [SerializeField] float moveSpeed = 2f;

	public List<Transform> GetWaypoints(){
        var waveWaypoints = new List<Transform>();
        // Debug.Log("in GetWaypoints()");
        foreach(Transform child in pathPrefab.transform) {
            // Debug.Log("adding child in GetWaypoints()");
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
	public float getTimeBetweenSpawns() { return this.timeBetweenSpawns;}
	public float getSpawnRandomFactor() {return this.spawnRandomFactor;}
	public int getNumberOfEnemies() { return this.numberOfEnemies;}
	public float getMoveSpeed() { return this.moveSpeed;}
    public GameObject getEnemyPrefab() { return enemyPrefab;}
}
