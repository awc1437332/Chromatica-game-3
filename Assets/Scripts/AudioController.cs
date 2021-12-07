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

public class AudioController : MonoBehaviour
{
    private BGAudioSource playerBGAudioScript;
    private SEAudioSource playerSEAudioScript;
    private bool collided;
    [SerializeField] private AudioClips audioClip;

    // Start is called before the first frame update
    void Start()
    {
        collided = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collided)
        {
            collided = true;

            if (audioClip == AudioClips.Bg
                || audioClip == AudioClips.Puzzle1
                || audioClip == AudioClips.Puzzle2
                || audioClip == AudioClips.Chase)
            {
                playerBGAudioScript = collider.GetComponentInChildren<BGAudioSource>();
                playerBGAudioScript.PlayAudio(audioClip); 
            }
            else
            {
                playerSEAudioScript = collider.GetComponentInChildren<SEAudioSource>();
                playerSEAudioScript.PlayAudio(audioClip);
            }
            //playerScript.PlayPuzzleSet1();
        }

        //if (collider.CompareTag("Player"))
        //{

        //}
    }
}
