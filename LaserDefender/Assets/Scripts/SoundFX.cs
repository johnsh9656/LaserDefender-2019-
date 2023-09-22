using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [Header("Game SFX")]
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0, 1)] float enemyDeathSoundVolume = 0.5f;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.75f;
    [SerializeField] AudioClip playerLaserSound;
    [SerializeField] [Range(0, 1)] float playerLaserSoundVolume = 0.1f;
    [SerializeField] AudioClip enemyLaserSound;
    [SerializeField] [Range(0, 1)] float enemyLaserSoundVolume = 0.4f;
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] [Range(0, 1)] float playerHitSoundVolume = 0.4f;
    [SerializeField] AudioClip enemyHitSound;
    [SerializeField] [Range(0, 1)] float enemyHitSoundVolume = 0.4f;
    [SerializeField] AudioClip healingSound;
    [SerializeField] [Range(0, 1)] float healingSoundVolume = 0.4f;
    [SerializeField] AudioClip waveLaserSound;
    [SerializeField] [Range(0, 1)] float waveLaserSoundVolume = 0.4f;
    [SerializeField] AudioClip hugeLaserSound;
    [SerializeField] [Range(0, 1)] float hugeLaserSoundVolume = 0.4f;

    [Header("Game Over SFX")]
    [SerializeField] AudioClip gameOverVoiceSound;
    [SerializeField] [Range(0, 1)] float gameOverVoiceSoundVolume = 0.2f;
    [SerializeField] AudioClip gameOverMusic;
    [SerializeField] [Range(0, 1)] float gameOverMusicVolume = 0.5f;

    [Header("Start Scene SFX")]
    [SerializeField] AudioClip playSelectSound;
    [SerializeField] [Range(0, 1)] float playSelectSoundVolume = 0.2f;
    [SerializeField] AudioClip musicOpening;
    [SerializeField] [Range(0, 1)] float musicOpeningVolume = 0.5f;
    [SerializeField] AudioClip jingle;
    [SerializeField] [Range(0, 1)] float jingleVolume = 0.5f;

    [SerializeField] AudioClip blipSound;
    [SerializeField] [Range(0, 1)] float blipSoundVolume = 0.5f;
    [SerializeField] AudioClip quitSound;
    [SerializeField] [Range(0, 1)] float quitSoundVolume = 0.5f;

    public void PlayQuitSound()
    {
        AudioSource.PlayClipAtPoint(quitSound, Camera.main.transform.position, quitSoundVolume);
    }

    public void PlayBlipSound()
    {
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
    }

    public void PlaySelectSound()
    {
        AudioSource.PlayClipAtPoint(playSelectSound, Camera.main.transform.position, playSelectSoundVolume);
    }

    public void StartMenuJingle()
    {
        AudioSource.PlayClipAtPoint(jingle, Camera.main.transform.position, jingleVolume);
    }

    public void MusicOpening()
    {
        AudioSource.PlayClipAtPoint(musicOpening, Camera.main.transform.position, musicOpeningVolume);
    }

    public void HealingSFX()
    {
        AudioSource.PlayClipAtPoint(healingSound, Camera.main.transform.position, healingSoundVolume);
    }

    public void EnemyDeathSFX()
    {
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, enemyDeathSoundVolume);
    }

    public void PlayerDeathSFX()
    {
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
    }

    public void PlayerLaserSFX()
    {
        AudioSource.PlayClipAtPoint(playerLaserSound, Camera.main.transform.position, playerLaserSoundVolume);
    }

    public void PlayerHitSFX()
    {
        AudioSource.PlayClipAtPoint(playerHitSound, Camera.main.transform.position, playerHitSoundVolume);
    }

    public void EnemyLaserSFX()
    {
        AudioSource.PlayClipAtPoint(enemyLaserSound, Camera.main.transform.position, enemyLaserSoundVolume);
    }

    public void EnemyHitSFX()
    {
        AudioSource.PlayClipAtPoint(enemyHitSound, Camera.main.transform.position, enemyHitSoundVolume);
    }

    public void GameOverVoiceSFX()
    {
        AudioSource.PlayClipAtPoint(gameOverVoiceSound, Camera.main.transform.position, gameOverVoiceSoundVolume);
    }

    public void GameOverMusic()
    {
        AudioSource.PlayClipAtPoint(gameOverMusic, Camera.main.transform.position, gameOverMusicVolume);
    }

    public void WaveLaserSFX()
    {
        AudioSource.PlayClipAtPoint(waveLaserSound, Camera.main.transform.position, waveLaserSoundVolume);
    }

}