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
    private bool collided;
    [SerializeField]
    private Light[] lights;
    private Flicker[] lightScripts;
    [SerializeField]
    private ControllerType type;
    [SerializeField]
    private float flickerDuration;

    // Start is called before the first frame update
    void Start()
    {
        collided = false;
        lightScripts = new Flicker[lights.Length];
        for (int i = 0; i < lightScripts.Length; i++)
        {
            lightScripts[i] = lights[i].GetComponent<Flicker>();
            //lightScripts[i].RegularFlickerDuration = regularFlickerDuration;
            //lightScripts[i].ChaseEndFlickerDuration = chaseEndFlickerDuration;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collided)
        {
            collided = true;

            switch (type)
            {
                case ControllerType.Flicker:
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(LightStates.Flickering);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                    }
                    break;
                case ControllerType.ChaseStart:
                    foreach (Flicker lightSource in lightScripts)
                        lightSource.ChangeStates(LightStates.ChaseStart);
                    break;
                case ControllerType.ChaseEnd:
                    foreach (Flicker lightSource in lightScripts)
                    {
                        lightSource.ChangeStates(LightStates.ChaseEnd);
                        lightSource.CurrentFlickerDuration = flickerDuration;
                    }
                    break;
            }
        }
    }
}
