using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    Flicker,
    IntermittentFlicker,
    ChaseStart,
    ChaseEnd
}

public class LightsController : MonoBehaviour
{
    /// <summary>
    /// Tracks whether this trigger has been collided with. Used to avoid 
    /// repeat triggers.
    /// </summary>
    private bool collided;

    /// <summary>
    /// References all affected lights in the scene.
    /// </summary>
    [SerializeField]
    private Light[] lights;

    /// <summary>
    /// References each affected light's Flicker script.
    /// </summary>
    private Flicker[] lightScripts;

    /// <summary>
    /// References each light's Audio Source component.
    /// </summary>
    private AudioSource[] lightAudio;

    /// <summary>
    /// Type of effect this controller has on affected lights when triggered.
    /// </summary>
    [SerializeField]
    private ControllerType type;

    /// <summary>
    /// How long to flicker affected lights for.
    /// </summary>
    [SerializeField]
    private float flickerDuration;

    /// <summary>
    /// Intensity of light source when power is restored.
    /// </summary>
    [SerializeField]
    private float finalIntensity;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise variables and set references.
        collided = false;
        lightScripts = new Flicker[lights.Length];
        lightAudio = new AudioSource[lights.Length];
        for (int i = 0; i < lightScripts.Length; i++)
        {
            lightScripts[i] = lights[i].GetComponent<Flicker>();
            lightAudio[i] = lights[i].GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (type == ControllerType.IntermittentFlicker)
        {
            float random = Random.Range(0.0f, 1.0f);
            if (random < 0.0005f)
            {
                Debug.Log("flicker everything!");
                //foreach (Flicker lightSource in lightScripts)
                for (int i = 0; i < lightScripts.Length; i++)
                {
                    lightScripts[i].ChangeStates(ControllerType.IntermittentFlicker);
                    lightScripts[i].CurrentFlickerDuration = flickerDuration;
                    lightAudio[i].PlayOneShot(lightAudio[i].clip);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Use a guard, so the controller can only be triggered once.
        if (!collided)
        {
            collided = true;

            // Based on type, alter all affected lights' states and set each
            // script's CurrentFlickerDuration property where applicable.
            switch (type)
            {
                case ControllerType.Flicker:
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(ControllerType.Flicker);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                    }
                    break;
                case ControllerType.IntermittentFlicker:
                    
                    break;
                case ControllerType.ChaseStart:
                    foreach (Flicker lightSource in lightScripts)
                        lightSource.ChangeStates(ControllerType.ChaseStart);
                    break;
                case ControllerType.ChaseEnd:
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(ControllerType.ChaseEnd);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                        lightSource.FinalIntensity = finalIntensity;
                    }
                    break;
            }
        }
    }
}
