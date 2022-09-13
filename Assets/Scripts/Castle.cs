using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] private int playerLife = 10;
    [SerializeField] private int damageCount = 1;
    [SerializeField] private Text lifeText;
    [SerializeField] private AudioClip castleDamageSoundFX;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        lifeText.text = playerLife.ToString();
    }

    public void Damage()
    {
        _audioSource.PlayOneShot(castleDamageSoundFX);
        playerLife -= damageCount;
        lifeText.text = playerLife.ToString();
    }
}
