using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource SFXPassosSource;

    public AudioSource PassosSource { get { return SFXPassosSource; } }

    [Header("---------Audio Clip---------")]
    public AudioClip background;
    public AudioClip attack;
    public AudioClip spawnTrashBag;
    public AudioClip takeDamage;    
    public AudioClip heal;
    public AudioClip telefone;
    public AudioClip telefonePickup;
    public AudioClip cartoonTalking;

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();    
        
         if(SFXPassosSource) SFXPassosSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);        
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }

}
