using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLaser : MonoBehaviour
{

    [SerializeField] float projectileSpeed = 25f;
    public GameObject waveLaserPrefab;

    public void FireWaveLaser()
    {
        Fire();
    }

    private void Fire()
    {
        GameObject laserWave = Instantiate
            (waveLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        laserWave.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        FindObjectOfType<SoundFX>().WaveLaserSFX();
    }

}
