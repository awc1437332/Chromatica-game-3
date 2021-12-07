using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private GameAudioSource playerAudioScript;
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

            playerAudioScript = collider.GetComponentInChildren<GameAudioSource>();
            playerAudioScript.PlayAudio(audioClip); 
            //playerScript.PlayPuzzleSet1();
        }

        //if (collider.CompareTag("Player"))
        //{

        //}
    }
}
