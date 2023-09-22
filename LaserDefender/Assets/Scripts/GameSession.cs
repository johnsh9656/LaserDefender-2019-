using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int currentLevel = 1;
    [SerializeField] int score = 0;
    [SerializeField] int level2Score = 10000;
    [SerializeField] int level3Score = 25000;
    [SerializeField] int level4Score = 40000;
    public GameObject level1Spawner;
    public GameObject level2Spawner;
    public GameObject level3Spawner;
    public GameObject hugeBoySpawner;
    public GameObject level4Spawner;
    public GameObject leaderSpawner;
    public GameObject bonusSpawner;

    [SerializeField] float levelWaitTime = 5f;
    [SerializeField] float startGameWaitTime = 2f;

    private void Awake()
    {
        SetUpGameSession();
    }

    public void SetUpGameSession()
    {
        StartCoroutine(Level1Spawners());
        RevertScore();
    }

    public void RevertScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        FindObjectOfType<ScoreKeeper>().ReceiveScore(score);
        CheckLevelUp();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void CheckLevelUp()
    {
        Debug.Log("Level Up Checked");
        if (currentLevel == 1)
        {
            if (score >= level2Score || score >= 10000)
            {
                if (score < level3Score || score < 25000)
                {
                    currentLevel++;
                    FindObjectOfType<PlayerScript>().LevelUp();
                    StartCoroutine(Level2Spawners());
                }
            }
        }
        if (currentLevel == 2)
        {
            if (score >= level3Score)
            {
                if (score < level4Score)
                {
                    Debug.Log("Player is level 3");
                    currentLevel++;
                    FindObjectOfType<PlayerScript>().LevelUp();
                    StartCoroutine(Level3Spawners());
                }
            }
        }
        if (currentLevel == 3)
        {
            if (score >= level4Score)
            {
                currentLevel++;
                FindObjectOfType<PlayerScript>().LevelUp();
                StartCoroutine(Level4Spawners());
            }
        }
    }

    public void StartBonusSpawners()
    {
        StartCoroutine(BonusSpawners());
    }

    IEnumerator Level1Spawners()
    {
        yield return new WaitForSeconds(startGameWaitTime);
        level1Spawner.SetActive(true);
    }

    IEnumerator Level2Spawners()
    {
        level1Spawner.SetActive(false);
        yield return new WaitForSeconds(levelWaitTime);
        level2Spawner.SetActive(true);
    }

    IEnumerator Level3Spawners()
    {
        level2Spawner.SetActive(false);
        yield return new WaitForSeconds(levelWaitTime);
        hugeBoySpawner.SetActive(true);
        yield return new WaitForSeconds(levelWaitTime);
        level3Spawner.SetActive(true);
    }

    IEnumerator Level4Spawners()
    {
        level3Spawner.SetActive(false);
        yield return new WaitForSeconds(levelWaitTime);
        leaderSpawner.SetActive(true);
        yield return new WaitForSeconds(levelWaitTime);
        level4Spawner.SetActive(true);
    }

    IEnumerator BonusSpawners()
    {
        level4Spawner.SetActive(false);
        yield return new WaitForSeconds(levelWaitTime);
        bonusSpawner.SetActive(true);
    }

}
