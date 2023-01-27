using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(levelChange());
        }
        
    }
    IEnumerator levelChange()
    {
        yield return new WaitForSecondsRealtime(delay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int lvls = currentScene + 1;
        if(lvls == SceneManager.sceneCountInBuildSettings)
        {
            lvls = 0;
        }
        SceneManager.LoadScene(currentScene + 1);

    }
}
