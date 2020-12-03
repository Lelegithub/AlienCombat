using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game Data/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Player Movement")]
    public float moveSpeed = 10f;

    public float padding = .05f;
    public int maxHealth = 200;

    [Header("Laser Projectile")]
    public GameObject laser;

    public float laserSpeed = 11f;
    public float projectileFiringPeriod = 0.1f;

    [Header("Player FX")]
    public AudioClip enemyExplosionSFX;

    public float explosionSoundVolume = 0.75f;
    public float shootSoundVolume = 0.75f;
    public AudioClip shootSound;

    public float invulnerablePeriod = 0.1f;
}