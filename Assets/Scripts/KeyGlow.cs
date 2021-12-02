using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGlow : MonoBehaviour
{
    private Material keyMaterial;

    [Min(1)] [SerializeField] public float glowIntensity;
    [Min(1)] [SerializeField] public float glowTime;
    private float glowTimer = 0;

    [Min(1)] float redGreen;

    private bool glowingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        //Set the material
        keyMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //Determines the current shade of the oscillation
        redGreen = glowTimer * glowIntensity;
        if (redGreen < 1)
        {
            redGreen = 1;
        }

        //Makes the color of the object oscillate between default and yellow
        keyMaterial.color = new Color(redGreen, redGreen, 1);

        //Manages the oscillation based on time
        if (glowingUp)
        {
            glowTimer += Time.deltaTime;

            if (glowTimer > glowTime)
            {
                glowingUp = false;
            }
        }
        else
        {
            glowTimer -= Time.deltaTime;

            if (glowTimer <= 0)
            {
                glowingUp = true;
            }     
        }
    }
}
