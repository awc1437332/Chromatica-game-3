using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for navmesh agent behaviour

public class AgentMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update agent destination each frame to account for player movement.
        agent.destination = target.position;
    }

    public void Activate(Vector3 _position)
    {
        transform.position = _position;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActiveAndEnabled)
        {
            if (collision.gameObject.tag == "Player")
            {
                //GameObject.Find("StateManager").GetComponent<StateManager>().EndGame();
            }
        }
    }
}
