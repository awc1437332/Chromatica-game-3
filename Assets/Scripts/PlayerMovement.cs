using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // To alter: Edit > Project Settings > Input Manager
        rb.AddForce(Vector3.forward * (Input.GetAxis("Vertical") * speed));
        rb.AddForce(Vector3.right * (Input.GetAxis("Horizontal") * speed));
    }
}
