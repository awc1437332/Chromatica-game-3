using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAudioSource : MonoBehaviour
{
    // BG + BGM
    [SerializeField] private AudioClip bg;
    [SerializeField] private AudioClip puzzle1;
    [SerializeField] private AudioClip puzzle2;
    [SerializeField] private AudioClip chase;

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
                audioSource.volume = 0.135f;
                audioSource.Play();
                Debug.Log("playing puzzle 1");
                break;
            case AudioClips.Puzzle2:
                audioSource.clip = puzzle2;
                audioSource.volume = 0.135f;
                audioSource.Play();
                Debug.Log("playing puzzle 2");
                break;
            case AudioClips.Chase:
                audioSource.clip = chase;
                audioSource.volume = 0.135f;
                audioSource.Play();
                Debug.Log("playing chase");
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