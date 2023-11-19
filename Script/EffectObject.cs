using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    public ParticleSystem burn;
    public AudioSource audioSource;
    public AudioClip clip;
    private bool particlePlay;
    void Start()
    {
        particlePlay = true;
        audioSource = GetComponent<AudioSource>();
        burn.Play();
        audioSource.PlayOneShot(clip);
    }

    void Update()
    {
        if(audioSource.isPlaying == false && particlePlay == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleSystemStopped()
    {
        particlePlay = false;
    }
}
