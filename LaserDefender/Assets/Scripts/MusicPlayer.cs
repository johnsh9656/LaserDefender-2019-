using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    AudioSource audioData;

    [SerializeField] float gameOverVoicePeriod = 1.5f;
    [SerializeField] float musicOpeningLength = 1.7f;

    void Start()
    {
        audioData = GetComponent<AudioSource>();

        if (tag == "GameOver")
        {
            FindObjectOfType<SoundFX>().GameOverVoiceSFX();
        }
        if (tag == "Game")
        {
            StartCoroutine(StartMusic());
        }
        if (tag == "StartScene")
        {
            FindObjectOfType<SoundFX>().StartMenuJingle();
        }
    }

    IEnumerator StartMusic()
    {
        FindObjectOfType<SoundFX>().MusicOpening();
        yield return new WaitForSeconds(musicOpeningLength);
        audioData.Play(0);

    }
}