using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All possible audio clips in the game + stop cue.
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
    // References to scripts controlling BG and SE audio.
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
        // Only allow trigger to activate once.
        if (!collided && collider.CompareTag("Player"))
        {
            collided = true;

            switch (audioClip)
            {
                // Play the audio using its associated script.
                case AudioClips.Bg:
                case AudioClips.Puzzle1:
                case AudioClips.Puzzle2:
                case AudioClips.Chase:
                    playerBGAudioScript = collider.GetComponentInChildren<BGAudioSource>();
                    playerBGAudioScript.PlayAudio(audioClip);
                    break;
                // Stop cue received. Fade out in playerBGAudioScript.
                case AudioClips.Stop:
                    playerBGAudioScript = collider.GetComponentInChildren<BGAudioSource>();
                    playerBGAudioScript.PlayAudio(audioClip);
                    break;
                default:
                    playerSEAudioScript = collider.GetComponentInChildren<SEAudioSource>();
                    playerSEAudioScript.PlayAudio(audioClip);
                    break;
            }
        }
    }
}
