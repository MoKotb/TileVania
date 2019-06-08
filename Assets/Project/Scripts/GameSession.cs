using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int playerLives = 3;
    [SerializeField]
    int playerScore = 0;

    private void Awake()
    {
        int numberOfGameSession = FindObjectsOfType<GameSession>().Length
;
        if (numberOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            FindObjectOfType<LevelLoader>().LoadMainMenu();
            ResetGameSession();
        }
    }

    public void ResetGameSession()
    {
        
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        FindObjectOfType<LevelLoader>().RestartScene();
    }

    public int GetLives()
    {
        return playerLives;
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void SetScore(int point)
    {
        playerScore += point;
    }
}
