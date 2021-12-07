using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClips
{
    Bg,
    Puzzle1,
    Puzzle2,
    Chase,
    PipeHiss,
    BalloonPop,
    PowerDown,
    PowerOn,
    LightFlickering,
    Warning
}

public class GameAudioSource : MonoBehaviour
{
    // BG + BGM
    [SerializeField] private AudioClip bg;
    [SerializeField] private AudioClip puzzle1;
    [SerializeField] private AudioClip puzzle2;
    [SerializeField] private AudioClip chase;

    // SE
    [SerializeField] private AudioClip pipeHiss;
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
        audioSource.loop = true;

        // Play atmospheric sound on start.
        PlayAudio(AudioClips.Bg);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClips audio)
    {
        switch (audio)
        {
            case AudioClips.Bg:
                audioSource.clip = bg;
                audioSource.volume = 1;
                audioSource.Play();
                Debug.Log("playing atmospheric");
                break;
            case AudioClips.Puzzle1:
                audioSource.clip = puzzle1;
                audioSource.volume = 0.75f;
                audioSource.Play();
                Debug.Log("playing puzzle 1");
                break;
            case AudioClips.LightFlickering:
                audioSource.clip = lightFlickering;
                audioSource.volume = 1;
                audioSource.PlayOneShot(audioSource.clip);
                Debug.Log("playing light flickering");
                break;
        }
    }

    //public void PlayPuzzleSet1()
    //{
    //    m_AudioSource.clip = puzzleSet1;
    //    m_AudioSource.PlayOneShot(m_AudioSource.clip);
    //    Debug.Log("playing puzzle 1");
    //}
}


// TODO NEXT: create separate gameobjects for se and bg audio