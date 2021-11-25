using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    /// <summary>
    /// The states room lighting can be in.
    /// </summary>
    private enum LightStates
    {
        Normal,
        Flickering,
        Chase
    }

    /// <summary>
    /// The minimum amount of time at which a flicker can last.
    /// </summary>
    [SerializeField]
    private float minFlickerSpeed;

    /// <summary>
    /// The maximum amount of time at which a flicker can last.
    /// </summary>
    [SerializeField]
    private float maxFlickerSpeed;

    /// <summary>
    /// Light source this script is attached to.
    /// </summary>
    private Light lightSource;

    /// <summary>
    /// Current state of this light source.
    /// </summary>
    private LightStates state;

    /// <summary>
    /// The initial intensity of the light source when the scene is loaded.
    /// </summary>
    private float defaultIntensity;

    /// <summary>
    /// Intensity of the light source when in a Chase state.
    /// </summary>
    [SerializeField]
    private float chaseIntensity;
    
    /// <summary>
    /// Amount of time the light source is turned off in the initial stages of
    /// the Chase sequence.
    /// </summary>
    [SerializeField]
    private float offPeriod;

    /// <summary>
    /// The factor by which the light source is gradually intensified. Used in 
    /// the Chase sequence.
    /// </summary>
    [SerializeField]
    private float intensifyFactor;

    /// <summary>
    /// Time between each intensity increment. Used in the Chase sequence.
    /// </summary>
    [SerializeField]
    private float intensifyInterval;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialise variables and set references.
        lightSource = GetComponent<Light>();
        state = LightStates.Normal;
        defaultIntensity = lightSource.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        // Key '1' toggles between Normal and Flickering states.
        if (Input.GetKeyDown(KeyCode.Alpha1) && state == LightStates.Normal)
        {
            StartCoroutine("FlickerLight");
            state = LightStates.Flickering;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && state == LightStates.Flickering)
        {
            StopCoroutine("FlickerLight");
            state = LightStates.Normal;

            // Avoid issue where coroutine is stopped while light source is disabled.
            lightSource.enabled = true;
        }

        // Key '2' toggles light source on/off.
        if (Input.GetKeyDown(KeyCode.Alpha2)) lightSource.enabled = !lightSource.enabled;

        // Key '3' toggles between Normal and Chase states.
        if (Input.GetKeyDown(KeyCode.Alpha3) && state == LightStates.Normal)
        {
            StartCoroutine("ChaseLight");
            state = LightStates.Chase;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && state == LightStates.Chase)
        {
            StopCoroutine("ChaseLight");

            // Enable light, and reset state, colour, and intensity.
            state = LightStates.Normal;
            lightSource.color = new Color(1.0f, 1.0f, 1.0f);
            lightSource.intensity = defaultIntensity;
            lightSource.enabled = true;
        }

        // Continuously flicker the lights if in the associated state.
        if (state == LightStates.Flickering) StartCoroutine("FlickerLight");
    }

    /// <summary>
    /// Flickers the light source.
    /// </summary>
    /// <returns>
    /// IEnumerator - necessary for coroutine setup.
    /// </returns>
    private IEnumerator FlickerLight()
    {
        // Turn on the light source for a random amount of time.
        lightSource.enabled = true;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));

        // Turn off the light source for a random amount of time.
        lightSource.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
    }

    /// <summary>
    /// Sequence that transitions the light source into Chase mode.
    /// </summary>
    /// <returns>
    /// IEnumerator - necessary for coroutine setup.
    /// </returns>
    private IEnumerator ChaseLight()
    {
        // Turn off the light source for a set period of time.
        lightSource.enabled = false;
        yield return new WaitForSeconds(offPeriod);

        // Turn down the intensity and change colour to red.
        // Enable the light source, so it can be gradually intensified in the
        // following while loop.
        lightSource.intensity = 0;
        lightSource.enabled = true;
        lightSource.color = new Color(1.0f, 0.0f, 0.0f);

        // Gradually intensify light source to intensity chaseIntensity.
        while (lightSource.intensity < chaseIntensity)
        {
            lightSource.intensity += intensifyFactor;
            yield return new WaitForSeconds(intensifyInterval);
        }
    }
}
