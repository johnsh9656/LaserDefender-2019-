using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float delayInSeconds = 3.5f;
    [SerializeField] float gameDelayInSeconds = 1f;

    public void LoadStartMenu()
    {
        StartCoroutine(LoadTheStartMenu());
    }

    IEnumerator LoadTheStartMenu()
    {
        FindObjectOfType<SoundFX>().PlayBlipSound();
        yield return new WaitForSeconds(gameDelayInSeconds);
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        StartCoroutine(LoadTheGame());
    }

    IEnumerator LoadTheGame()
    {
        FindObjectOfType<SoundFX>().PlaySelectSound();
        yield return new WaitForSeconds(gameDelayInSeconds);
        SceneManager.LoadScene("Game");
        FindObjectOfType<ScoreKeeper>().RevertScore();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void LoadGuideScene()
    {
        SceneManager.LoadScene("Guide");
        FindObjectOfType<SoundFX>().PlayBlipSound();
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator QuitTheGame()
    {
        FindObjectOfType<SoundFX>().PlayQuitSound();
        yield return new WaitForSeconds(gameDelayInSeconds);
        Application.Quit();
    }

}
