using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int Lifes = 3;
    void Awake()
    {
        int GamesecLen = FindObjectsOfType<GameSession>().Length;
        if (GamesecLen > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void processPlayerDeath()
    {
        if(Lifes > 1)
        {
            Takelife();
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void Takelife()
    {
        Lifes--;
        int curr = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curr);
    }
}
