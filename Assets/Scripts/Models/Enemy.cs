//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float health = 100;

    [SerializeField] private int scoreValue = 100;

    private float shotCounter;

    [Header("Enemy Shooting")]
    [SerializeField] private float minTimeBetweenShots = 0.2f;

    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject laser;
    [SerializeField] private float projectileSpeed = 10f;

    [Header("Enemy FX")]
    [SerializeField] private GameObject particleVFX;

    [SerializeField] private float deathVFXTime = 1f;
    [SerializeField] private AudioClip enemyExplosionSFX;
    [SerializeField] [Range(0, 1)] private float explosionSoundVolume = 0.75f;
    [SerializeField] [Range(0, 1)] private float shootSoundVolume = 0.75f;
    [SerializeField] private AudioClip shootSound;

    // Start is called before the first frame update
    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    private void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject firedLaser = Instantiate(this.laser,
            transform.position,
            Quaternion.identity) as GameObject;
        firedLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // Prevent Null Reference Exception if game object has no DamageDealer Component
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(other, damageDealer);
    }

    private void ProcessHit(Collider2D other, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die(other);
        }
    }

    private void Die(Collider2D other)
    {
        GameController g = FindObjectOfType<GameController>();
        if (g != null) { g.AddToScore(scoreValue); }
        Destroy(this.gameObject);
        Destroy(other.gameObject);
        GameObject explosionVFX = Instantiate(particleVFX, transform.position, transform.rotation);
        Destroy(explosionVFX, deathVFXTime);
        AudioSource.PlayClipAtPoint(enemyExplosionSFX, Camera.main.transform.position, explosionSoundVolume);
    }
}