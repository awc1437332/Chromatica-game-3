using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    public float keyCount;

    private ReticleRaycast reticleScript;

    //ReticleRaycast reticle;

    // Start is called before the first frame update
    void Start()
    {
        keyCount = 0;
        reticleScript = GetComponent<ReticleRaycast>();

        //reticle = GameObject.FindGameObjectWithTag("FPSController").GetComponent<ReticleRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            reticleScript.Cast();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Key")
    //    {
    //        Debug.Log("You have collided with a key");
    //        Destroy(gameObject);

    //        keyCount++;
    //    }
    //}
}
