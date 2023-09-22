using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{

    [SerializeField] bool initialized = false;

    [Header("SFX")]
    [SerializeField] AudioClip blipSound;
    [SerializeField] [Range(0, 1)] float blipSoundVolume = 0.3f;
    [SerializeField] AudioClip completeSound;
    [SerializeField] [Range(0, 1)] float completeSoundVolume = 0.3f;
    [SerializeField] AudioClip errorSound;
    [SerializeField] [Range(0, 1)] float errorSoundVolume = 0.2f;

    [Header("Text")]
    public GameObject headingText;
    public GameObject subheadingText;
    public GameObject spritesText;
    public GameObject scriptsText;
    public GameObject musicText;
    public GameObject sfxText;
    public GameObject enemiesText;
    public GameObject playerText;
    public GameObject gameReadyText;
    public GameObject pressText;

    [Header("WaitTimes")]
    [SerializeField] float initialWaitTime = 7f;
    [SerializeField] float longerWaitTime = 4f;
    [SerializeField] float shorterWaitTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeItemsAppear());
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (initialized)
            {
                StartCoroutine(StartGame());
            }
            else
            {
                AudioSource.PlayClipAtPoint(errorSound, Camera.main.transform.position, errorSoundVolume);
            }
        }
    }

    IEnumerator StartGame()
    {
        AudioSource.PlayClipAtPoint(completeSound, Camera.main.transform.position, completeSoundVolume);
        yield return new WaitForSeconds(longerWaitTime);
        FindObjectOfType<Level>().LoadStartMenu();
    }

    IEnumerator MakeItemsAppear()
    {
        yield return new WaitForSeconds(initialWaitTime);
        headingText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(longerWaitTime);
        subheadingText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(longerWaitTime);
        spritesText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(shorterWaitTime);
        scriptsText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(shorterWaitTime);
        musicText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(shorterWaitTime);
        sfxText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(shorterWaitTime);
        enemiesText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(shorterWaitTime);
        playerText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(longerWaitTime);
        gameReadyText.SetActive(true);
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
        yield return new WaitForSeconds(longerWaitTime);
        pressText.SetActive(true);
        initialized = true;
        AudioSource.PlayClipAtPoint(blipSound, Camera.main.transform.position, blipSoundVolume);
    }
}
