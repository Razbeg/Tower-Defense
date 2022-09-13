using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int hitPoints = 10;
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private AudioClip hitEnemySoundFX;
    [SerializeField] private AudioClip deathEnemySoundFX;

    private AudioSource _audioSource;
    
    private Text _scoreText;
    private int _currentScore;
    private Camera _camera;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _camera = Camera.main;
        _scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            DestroyEnemy(deathParticles, true);
        }
    }

    private void ProcessHit()
    {
        _audioSource.PlayOneShot(hitEnemySoundFX);
        hitParticles.Play();
        hitPoints -= 1;
    }

    public void DestroyEnemy(ParticleSystem fx, bool addScore)
    {
        if (addScore)
        {
            _currentScore = int.Parse(_scoreText.text);
            _currentScore++;
            _scoreText.text = _currentScore.ToString();
        }
        
        var destroyFX = Instantiate(fx, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathEnemySoundFX, _camera.transform.position);
        destroyFX.Play();
        Destroy(gameObject);
    }
}
