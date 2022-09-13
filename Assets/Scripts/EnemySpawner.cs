using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 20f)] private float spawnInterval;
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private AudioClip enemySpawnSoundFX;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (true)
        {
            _audioSource.PlayOneShot(enemySpawnSoundFX);
            Instantiate(enemyPrefab.gameObject, transform);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
