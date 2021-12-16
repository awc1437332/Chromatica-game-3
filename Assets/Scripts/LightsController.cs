using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum ControllerType
{
    Flicker,
    IntermittentFlicker,
    ChaseStart,
    ChaseEnd,
    ChaseEndSpecial
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

    [SerializeField]
    private GameObject agent;
    private AgentMovement agentScript;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise variables and set references.
        collided = false;
        lightScripts = new Flicker[lights.Length];
        lightAudio = new AudioSource[lights.Length];
        agentScript = agent.GetComponent<AgentMovement>();

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

    private void MoveAgentToPlayer(GameObject player)
    {
        // teleport agent to behind the player and ensure it faces them when
        // they turn around.
        agent.transform.position = player.transform.position - new Vector3(10.0f, -1.5f, 0);
        agent.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        //agent.transform.position = new Vector3(43.3f, 1, -315.56f);

        agent.SetActive(true);
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
                    // Activate the monster.
                    agentScript.Activate(transform.position 
                                        - new Vector3(10.0f, -1.5f, 0));
                    foreach (Flicker lightSource in lightScripts)
                        lightSource.ChangeStates(ControllerType.ChaseStart);
                    break;
                case ControllerType.ChaseEnd:
                    // Deactivate the monster.
                    agentScript.Deactivate();
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(ControllerType.ChaseEnd);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                        lightSource.FinalIntensity = finalIntensity;
                    }
                    break;
                case ControllerType.ChaseEndSpecial:
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(ControllerType.ChaseEnd);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                        lightSource.FinalIntensity = finalIntensity;
                    }

                    GameObject player = GameObject.Find("FPSController");
                    FirstPersonController playerScript = player.GetComponent<FirstPersonController>();

                    // Stop the agent from moving
                    agentScript.Stop();
                    //agent.SetActive(false);

                    // move the player camera. quickly to face the door
                    playerScript.isActive = false;
                    StartCoroutine(playerScript.FaceDoor());

                    //Invoke()

                    // then move slowly to face behind
                    break;
            }
        }
    }
}
