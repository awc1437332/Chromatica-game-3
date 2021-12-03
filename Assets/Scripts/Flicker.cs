using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
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
    /// Whether this light source is currently flickering.
    /// </summary>
    private bool isFlickering;

    /// <summary>
    /// Determines whether the light source is flickering permanently.
    /// </summary>
    [SerializeField]
    private bool isFlickeringPermanently;

    /// <summary>
    /// Determines whether the light source is flickering intermittently.
    /// </summary>
    private bool isFlickeringIntermittently;

    public float CurrentFlickerDuration { get; set; }

    /// <summary>
    /// Duration that the light source has been flickering for.
    /// </summary>
    private float flickerDurationCounter;

    /// <summary>
    /// Intensity of light source when power is restored.
    /// </summary>
    public float FinalIntensity { get; set; }

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
    private float chaseStartOffPeriod;

    /// <summary>
    /// Amount of time the light source is turned off in the ending stages of
    /// the Chase sequence.
    /// </summary>
    [SerializeField]
    private float chaseEndOffPeriod;

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
        isFlickering = false;
        FinalIntensity = lightSource.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickeringPermanently)
            //StartCoroutine("FlickerLight");
            if (Random.Range(0.0f, 1.0f) < 0.025f) StartCoroutine("FlickerLight");
        else if (isFlickeringIntermittently)
            // Randomly flicker the light source.
            if (Random.Range(0.0f, 1.0f) < 0.1f) StartCoroutine("FlickerLight");
        if (isFlickering)
        {
            // Flicker lights until enough time has passed.
            if (flickerDurationCounter < CurrentFlickerDuration)
            {
                flickerDurationCounter += Time.deltaTime;
                StartCoroutine("FlickerLight");
            }
            else
            {
                flickerDurationCounter = 0;
                isFlickering = false;
            }
        }

        #region Lighting Testing
        //// Key '1' toggles between Normal and Flickering states.
        //if (Input.GetKeyDown(KeyCode.Alpha1) && State == LightStates.Normal)
        //{
        //    StartCoroutine("FlickerLight");
        //    State = LightStates.Flickering;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha1) && State == LightStates.Flickering)
        //{
        //    StopCoroutine("FlickerLight");
        //    State = LightStates.Normal;

        //    // Avoid issue where coroutine is stopped while light source is disabled.
        //    lightSource.enabled = true;
        //}

        //// Key '2' toggles light source on/off.
        //if (Input.GetKeyDown(KeyCode.Alpha2)) lightSource.enabled = !lightSource.enabled;

        //// Key '3' toggles between Normal and Chase states.
        //if (Input.GetKeyDown(KeyCode.Alpha3) && State == LightStates.Normal)
        //{
        //    StartCoroutine("ChaseLight");
        //    State = LightStates.Chase;
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3) && State == LightStates.Chase)
        //{
        //    StopCoroutine("ChaseLight");

        //    // Enable light, and reset state, colour, and intensity.
        //    State = LightStates.Normal;
        //    lightSource.color = new Color(1.0f, 1.0f, 1.0f);
        //    lightSource.intensity = defaultIntensity;
        //    lightSource.enabled = true;
        //}

        //// Continuously flicker the lights if in the associated state.
        //if (State == LightStates.Flickering) StartCoroutine("FlickerLight");
        #endregion
    }

    /// <summary>
    /// Exposes to external classes a way to change the current state of the 
    /// light source.
    /// </summary>
    /// <param name="lightState">
    /// State to change to.
    /// </param>
    public void ChangeStates(ControllerType lightState)
    {
        // Play the Chase sequences for lightState ChaseStart/End.
        // Alter isFlickering for lightState Flicker.
        switch (lightState)
        {
            case ControllerType.ChaseStart:
                StartCoroutine("ChaseLightStart");
                break;
            case ControllerType.ChaseEnd:
                StartCoroutine("ChaseLightEnd");
                break;
            case ControllerType.Flicker:
                isFlickering = true;
                break;
            case ControllerType.IntermittentFlicker:
                if (!isFlickeringPermanently) isFlickeringIntermittently = true;
                break;
        }
    }

    /// <summary>
    /// Flickers the light source.
    /// </summary>
    /// <returns>
    /// IEnumerator - necessary for coroutine setup.
    /// </returns>
    private IEnumerator FlickerLight()
    {
        // Turn off the light source for a random amount of time.
        lightSource.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        
        // Turn on the light source for a random amount of time.
        lightSource.enabled = true;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
    }

    /// <summary>
    /// Sequence that transitions the light source into Chase mode.
    /// </summary>
    /// <returns>
    /// IEnumerator - necessary for coroutine setup.
    /// </returns>
    private IEnumerator ChaseLightStart()
    {
        // Turn off the light source for a set period of time.
        lightSource.enabled = false;
        yield return new WaitForSeconds(chaseStartOffPeriod);

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

    private IEnumerator ChaseLightEnd()
    {
        // Turn off the light source for a set period of time.
        lightSource.enabled = false;
        yield return new WaitForSeconds(chaseEndOffPeriod);

        // Turn down the intensity and change colour to white.
        // Enable the light source, so it can be gradually intensified in the
        // following while loop.
        lightSource.intensity = 0;
        lightSource.enabled = true;
        lightSource.color = new Color(1.0f, 1.0f, 1.0f);

        // Gradually intensify light source to intensity defaultIntensity.
        while (lightSource.intensity < FinalIntensity)
        {
            // Randomly flicker the light source.
            if (Random.Range(0.0f, 1.0f) < 0.1f) StartCoroutine("FlickerLight");

            lightSource.intensity += intensifyFactor;
            yield return new WaitForSeconds(intensifyInterval);
        }

        // Flicker lights for a short while they return to full intensity.
        yield return new WaitForSeconds(chaseEndOffPeriod);
        isFlickering = true;
    }
}
