using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        GameObject uICanvas = GameObject.Find("UI/Canvas");
        uICanvas.GetComponent<Canvas>().worldCamera = gameObject.transform.Find("FirstPersonCharacter").GetComponent<Camera>();
        
        //reticle = GameObject.FindGameObjectWithTag("FPSController").GetComponent<ReticleRaycast>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            reticleScript.Cast(true);
        }
        else
        {
            reticleScript.Cast(false);
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
