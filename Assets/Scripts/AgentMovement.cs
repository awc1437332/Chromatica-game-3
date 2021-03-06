using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for navmesh agent behaviour
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class AgentMovement : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    private Material monsterMaterial;

    public bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameObject.SetActive(false);

        monsterMaterial = GetComponent<Renderer>().material;
        monsterMaterial.color = new Color(0, 0, 0);

        // Hook up callbacks to events.
        //showAgent.AddListener(Activate);
        //hideAgent.AddListener(Deactivate);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            // Update agent destination each frame to account for player movement.
            agent.destination = target.position;
        }
    }

    /// <summary>
    /// Activates the monster and sets its location relative to the player.
    /// </summary>
    /// <param name="_position"></param>
    public void Activate(Vector3 _position)
    {
        transform.position = _position;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Deactivates the monster.
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Stop()
    {
        isActive = false;
        agent.destination = agent.transform.position;
        agent.speed = 0;
        agent.isStopped = true;
    }

    public void SetColor(Color _color)
    {
        if (!monsterMaterial) monsterMaterial = GetComponent<Renderer>().material;
        monsterMaterial.color = _color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isActiveAndEnabled)
        {
            if (collision.gameObject.tag == "Player")
            {
                //GameObject.Find("StateManager").GetComponent<StateManager>().EndGame();
                Cursor.visible = true;
                SceneManager.LoadScene("gameOverScene");

                Debug.Log("player detected");
            }
        }
    }
}
