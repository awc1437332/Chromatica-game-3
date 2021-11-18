using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float keyCount;

    ReticleRaycast reticle;

    // Start is called before the first frame update
    void Start()
    {
        keyCount = 0;

        reticle = GameObject.FindGameObjectWithTag("FPSController").GetComponent<ReticleRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse pressed");
        }

        reticle.FixedUpdate();
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
