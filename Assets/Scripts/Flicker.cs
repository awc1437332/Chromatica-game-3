using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    [SerializeField]
    private float minFlickerSpeed;
    [SerializeField]
    private float maxFlickerSpeed;
    private Light lightSource;
    
    // Start is called before the first frame update
    void Start()
    {
        lightSource = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("FlickerLight");
    }

    private IEnumerator FlickerLight()
    {
        //Debug.Log("enabling light");
        lightSource.enabled = true;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        //yield return new WaitForSeconds(5);
        //Debug.Log("disabling light");
        lightSource.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        //yield return new WaitForSeconds(5);
    }
}
