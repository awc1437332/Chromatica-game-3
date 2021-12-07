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
    Warning,
    Stop
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

            switch (audioClip)
            {
                case AudioClips.Bg:
                case AudioClips.Puzzle1:
                case AudioClips.Puzzle2:
                case AudioClips.Chase:
                    playerBGAudioScript = collider.GetComponentInChildren<BGAudioSource>();
                    playerBGAudioScript.PlayAudio(audioClip);
                    break;
                case AudioClips.Stop:
                    playerBGAudioScript = collider.GetComponentInChildren<BGAudioSource>();
                    playerBGAudioScript.PlayAudio(audioClip);
                    break;
                default:
                    playerSEAudioScript = collider.GetComponentInChildren<SEAudioSource>();
                    playerSEAudioScript.PlayAudio(audioClip);
                    break;
            }
            //playerScript.PlayPuzzleSet1();
        }

        //if (collider.CompareTag("Player"))
        //{

        //}
    }
}
