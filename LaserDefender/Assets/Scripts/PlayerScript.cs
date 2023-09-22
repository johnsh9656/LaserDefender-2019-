using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //config parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 4f; //4f
    [SerializeField] float padding = 0.5f;
    [SerializeField] float topPadding = 10f;
    [SerializeField] int health = 3; //3
    [SerializeField] int maxHealth = 3;
    [SerializeField] float shipBlinkTimeInSec = 0.1f;

    [Header("Health")]
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject life4;
    public GameObject life5;
    public GameObject life6;
    public GameObject lostLife4;
    public GameObject lostLife5;
    public GameObject lostLife6;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject laserPrefab2;
    public GameObject laserPrefab3;
    public GameObject laserPrefab4;
    [SerializeField] float projectileSpeed = 22.5f; // 22.5f
    [SerializeField] float projectileFiringPeriod = 0.125f; // .125f

    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Lightning Box")]
    [SerializeField] float lightningMoveSpeed = 5f;
    [SerializeField] float lightningProjectileSpeed = 30f;
    [SerializeField] float lightningProjectileFiringPeriod = 0.01f;
    public GameObject lightningLaserPrefab;
    [SerializeField] float lightningDuration = 10f;
    public bool invincible = false;

    [Header("Current Stats")]
    public GameObject currentLaser;
    [SerializeField] float currentMoveSpeed;
    [SerializeField] float currentProjectileSpeed;
    [SerializeField] float currentProjectileFiringPeriod;
    [SerializeField] int currentLevel = 1;
    public GameObject backupLaserPrefab;

    [Header("WaveLaser")]
    [SerializeField] float shotCounter;
    [SerializeField] float shotCounterMax;
    [SerializeField] float level1ShotCounter = 18f;
    [SerializeField] float level2ShotCounter = 14f;
    [SerializeField] float level3ShotCounter = 10f;
    [SerializeField] float level4ShotCounter = 7f;
    public GameObject waveLaserPrefab;
    public bool waveLaserAvailable = false;
    [SerializeField] float waveLaserProjectileSpeed = 15f;
    public GameObject checkMark;
    public GameObject redX;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    [SerializeField] Color shipBlinkColor;
    [SerializeField] Color lightningColor;

    void Start()
    {
        SetUpMoveBoundaries();
        ResetPlayer();
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        CountDownForWaveLaser();
        CheckWaveLaser();
    }

    private void CountDownForWaveLaser()
    {
        if (shotCounter > 0)
        {
            shotCounter -= Time.deltaTime;
        }
        if (shotCounter <= 0f)
        {
            waveLaserAvailable = true;
            checkMark.SetActive(true);
            redX.SetActive(false);
        }
    }

    private void CheckWaveLaser()
    {
        if (waveLaserAvailable)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GameObject laserWave = Instantiate
            (waveLaserPrefab, transform.position, Quaternion.identity) as GameObject;
                laserWave.GetComponent<Rigidbody2D>().velocity = new Vector2(0, waveLaserProjectileSpeed);
                FindObjectOfType<SoundFX>().WaveLaserSFX();

                waveLaserAvailable = false;

                redX.SetActive(true);
                checkMark.SetActive(false);

                FindObjectOfType<SoundFX>().WaveLaserSFX();

                shotCounter = shotCounterMax;
            }
        }
    }

    public void LevelUp()
    {
        currentLevel = FindObjectOfType<GameSession>().GetCurrentLevel();
        maxHealth++;
        Debug.Log("Level Up");
        if (currentLevel == 2)
        {
            projectileFiringPeriod = 0.1175f;
            currentLaser = laserPrefab2;
            backupLaserPrefab = currentLaser;
            lostLife4.SetActive(true);
            shotCounter = level2ShotCounter;
            shotCounterMax = level2ShotCounter;
        }
        if (currentLevel == 3)
        {
            projectileFiringPeriod = 0.085f;
            projectileSpeed = 25f;
            currentLaser = laserPrefab3;
            backupLaserPrefab = currentLaser;
            lostLife4.SetActive(true);
            lostLife5.SetActive(true);
            shotCounter = level3ShotCounter;
            shotCounterMax = level3ShotCounter;
        }
        if (currentLevel == 4)
        {
            projectileFiringPeriod = 0.07f;
            projectileSpeed = 27.5f;
            currentLaser = laserPrefab4;
            backupLaserPrefab = currentLaser;
            lostLife4.SetActive(true);
            lostLife5.SetActive(true);
            lostLife6.SetActive(true);
            shotCounter = level4ShotCounter;
            shotCounterMax = level4ShotCounter;
        }

        AddHealth();
    }

    public void AddHealth()
    {
        if (health < maxHealth)
        {
            health++;
        }

        if (health == 2)
        {
            life2.SetActive(true);
        }
        if (health == 3)
        {
            life2.SetActive(true);
            life3.SetActive(true);
        }
        if (health == 4)
        {
            life2.SetActive(true);
            life3.SetActive(true);
            life4.SetActive(true);
        }
        if (health == 5)
        {
            life2.SetActive(true);
            life3.SetActive(true);
            life4.SetActive(true);
            life5.SetActive(true);
        }
        if (health == 6)
        {
            life2.SetActive(true);
            life3.SetActive(true);
            life4.SetActive(true);
            life5.SetActive(true);
            life6.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        damageDealer.Hit();
        if (!invincible)
        {
            health -= damageDealer.GetDamage();
            if (health >= maxHealth)
            {
                health = maxHealth;
            }
            if (health == 5)
            {
                life6.SetActive(false);
                life5.SetActive(true);
                FindObjectOfType<SoundFX>().PlayerHitSFX();
                StartCoroutine(ShipBlink());
            }
            if (health == 4)
            {
                life6.SetActive(false);
                life5.SetActive(false);
                life4.SetActive(true);
                FindObjectOfType<SoundFX>().PlayerHitSFX();
                StartCoroutine(ShipBlink());
            }
            if (health == 3)
            {
                life5.SetActive(false);
                life4.SetActive(false);
                life3.SetActive(true);
                FindObjectOfType<SoundFX>().PlayerHitSFX();
                StartCoroutine(ShipBlink());
            }
            if (health == 2)
            {
                life4.SetActive(false);
                life3.SetActive(false);
                life2.SetActive(true);
                FindObjectOfType<SoundFX>().PlayerHitSFX();
                StartCoroutine(ShipBlink());
            }
            if (health == 1)
            {
                life3.SetActive(false);
                life2.SetActive(false);
                life1.SetActive(true);
                FindObjectOfType<SoundFX>().PlayerHitSFX();
                StartCoroutine(ShipBlink());
            }
            if (health <= 0)
            {
                life2.SetActive(false);
                life1.SetActive(false);
                Die();
            }
        }
    }

    public void ResetPlayer()
    {
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        currentMoveSpeed = moveSpeed;
        currentProjectileSpeed = projectileSpeed;
        currentProjectileFiringPeriod = projectileFiringPeriod;
        invincible = false;
        maxHealth = health;
        shotCounter = level1ShotCounter;
        shotCounterMax = level1ShotCounter;
    }

    private void Die()
    {
        Destroy(gameObject);
        FindObjectOfType<SoundFX>().PlayerDeathSFX();
        FindObjectOfType<Level>().LoadGameOver();
        GameObject deathExplosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Instantiate(deathExplosion);
        Destroy(deathExplosion, durationOfExplosion);
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        {
            while (true)
            {
                GameObject laser = Instantiate(currentLaser, transform.position, Quaternion.identity) as GameObject;
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, currentProjectileSpeed);
                FindObjectOfType<SoundFX>().PlayerLaserSFX();
                yield return new WaitForSeconds(currentProjectileFiringPeriod);
            }
        }
    }

    private IEnumerator ShipBlink()
    {
        GetComponent<SpriteRenderer>().color = shipBlinkColor;
        yield return new WaitForSeconds(shipBlinkTimeInSec);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator AllStar()
    {
        currentMoveSpeed = lightningMoveSpeed;
        currentProjectileFiringPeriod = lightningProjectileFiringPeriod;
        currentProjectileSpeed = lightningProjectileSpeed;
        currentLaser = lightningLaserPrefab;
        invincible = true;
        GetComponent<SpriteRenderer>().color = lightningColor;
        yield return new WaitForSeconds(lightningDuration);
        currentMoveSpeed = moveSpeed;
        currentProjectileFiringPeriod = projectileFiringPeriod;
        currentProjectileSpeed = projectileSpeed;
        currentLaser = backupLaserPrefab;
        invincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void LightningBox()
    {
        StartCoroutine(AllStar());
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - topPadding;
    }

}