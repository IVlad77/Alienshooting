using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    float levelLoadDelay = 1f;

    [SerializeField]
    GameObject deathSound;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathSound.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
       
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
