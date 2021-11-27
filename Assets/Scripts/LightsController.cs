using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType
{
    Flicker,
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
    /// Type of effect this controller has on affected lights when triggered.
    /// </summary>
    [SerializeField]
    private ControllerType type;

    /// <summary>
    /// How long to flicker affected lights for.
    /// </summary>
    [SerializeField]
    private float flickerDuration;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise variables and set references.
        collided = false;
        lightScripts = new Flicker[lights.Length];
        for (int i = 0; i < lightScripts.Length; i++)
            lightScripts[i] = lights[i].GetComponent<Flicker>();
    }

    // Update is called once per frame
    void Update()
    {

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
                case ControllerType.ChaseStart:
                    foreach (Flicker lightSource in lightScripts)
                        lightSource.ChangeStates(ControllerType.ChaseStart);
                    break;
                case ControllerType.ChaseEnd:
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(ControllerType.ChaseEnd);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                    }
                    break;
            }
        }
    }
}
