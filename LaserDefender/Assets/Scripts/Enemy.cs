using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject deathVFX;

    [Header("Enemy Stats")]
    [SerializeField] float maxHealth = 1;
    [SerializeField] float health = 1;
    [SerializeField] int scoreValue = 150;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = -15f;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] float shipBlinkTimeInSec = 0.1f;
    [SerializeField] Color shipBlinkColor;
    [SerializeField] bool hugeBoy = false;
    [SerializeField] bool healer = false;
    [SerializeField] bool meteor = false;
    [SerializeField] bool leader = false;

    [SerializeField] AudioClip hugeBoyLaser;
    [SerializeField] [Range(0, 1)] float hugeBoyLaserVolume = 0.3f;
    [SerializeField] AudioClip healerShotSound;
    [SerializeField] [Range(0, 1)] float healerShotSoundVolume = 0.4f;

    [SerializeField] float minMeteorHealth = 10f;
    [SerializeField] float maxMeteorHealth = 30f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        if (meteor)
        {
            health = Random.Range(minMeteorHealth, maxMeteorHealth);
            maxHealth = health;
        }
        else
        {
            maxHealth = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        if (hugeBoy)
        {
            HugeBoyLaserSFX();
        }
        else if (healer)
        {
            HealerShotSFX();
        }
        else
        {
            FindObjectOfType<SoundFX>().EnemyLaserSFX();
        }
    }

    public void HugeBoyLaserSFX()
    {
        AudioSource.PlayClipAtPoint(hugeBoyLaser, Camera.main.transform.position, hugeBoyLaserVolume);
    }

    public void HealerShotSFX()
    {
        AudioSource.PlayClipAtPoint(healerShotSound, Camera.main.transform.position, healerShotSoundVolume);
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            Die();
        }
        else
        {
            FindObjectOfType<SoundFX>().EnemyHitSFX();
            StartCoroutine(ShipBlink());
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        CheckDeathSFX();
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Instantiate(explosion);
        Destroy(explosion, durationOfExplosion);
        if (leader)
        {
            FindObjectOfType<GameSession>().StartBonusSpawners();
        }
        if (meteor)
        {
            FindObjectOfType<PlayerScript>().LightningBox();
        }
        if (healer)
        {
            FindObjectOfType<SoundFX>().HealingSFX();
        }
    }

    private void CheckDeathSFX()
    {
        if (maxHealth == 1)
        {
            FindObjectOfType<SoundFX>().EnemyHitSFX();
        }
        else
        {
            FindObjectOfType<SoundFX>().EnemyDeathSFX();
        }
    }

    private IEnumerator ShipBlink()
    {
        GetComponent<SpriteRenderer>().color = shipBlinkColor;
        yield return new WaitForSeconds(shipBlinkTimeInSec);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

}