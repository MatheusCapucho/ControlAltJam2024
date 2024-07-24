using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRelated : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnPlayerDetected += RestartFromCheckPoint;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDetected -= RestartFromCheckPoint;
    }

    private void RestartFromCheckPoint()
    {
        StartCoroutine(RestartAfterSeconds());
    }

    IEnumerator RestartAfterSeconds()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
