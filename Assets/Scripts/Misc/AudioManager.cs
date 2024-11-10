using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip background;
    public AudioClip attack;
    public AudioClip spawnTrashBag;
    public AudioClip takeDamage;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);        
    }

}
