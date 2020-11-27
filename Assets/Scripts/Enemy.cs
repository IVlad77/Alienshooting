using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int scorePerHit = 12;

    [SerializeField]
    GameObject deathEffect;

    [SerializeField]
    Transform parent;

    [SerializeField]
    int maxHits = 10;

    ScoreBoard scoreBoard;
    private void Start()
    {
        AddNonTriggerBoxCollider();
    }

    void AddNonTriggerBoxCollider()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (maxHits <= 100)
        {
            KillEnemy();
        }

    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        maxHits--;
    }

    private void KillEnemy()
    {
        GameObject effects = Instantiate(deathEffect, transform.position, Quaternion.identity);
        effects.transform.parent = parent;
        Destroy(gameObject);
    }
}
