using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SEAudioSource : MonoBehaviour
{
    // SE
    [SerializeField] private AudioClip[] pipeHiss;
    [SerializeField] private AudioClip balloonPop;
    [SerializeField] private AudioClip powerDown;
    [SerializeField] private AudioClip powerOn;
    [SerializeField] private AudioClip lightFlickering;
    [SerializeField] private AudioClip warning;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClips audio)
    {
        // Play the audio using its associated script.
        // Use PlayOneShot() to prevent interference with BG audio, and also
        // because these effects do not need to be looped.
        switch (audio)
        {
            case AudioClips.PipeHiss:
                audioSource.clip = pipeHiss[Random.Range(0, pipeHiss.Length)];
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing pipe hiss");
                break;
            case AudioClips.BalloonPop:
                audioSource.clip = balloonPop;
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing balloon pop");
                break;
            case AudioClips.PowerDown:
                audioSource.clip = powerDown;
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing power down");
                break;
            case AudioClips.PowerOn:
                audioSource.clip = powerOn;
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing power on");
                break;
            case AudioClips.LightFlickering:
                audioSource.clip = lightFlickering;
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing light flickering");
                break;
            case AudioClips.Warning:
                audioSource.clip = warning;
                audioSource.volume = 0.5f;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing warning");
                break;
        }
    }
}