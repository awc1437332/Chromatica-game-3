using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject spawn;
    private Vector3 spawnLocation;
    private Vector3 monsterSpawnLocation;

    [SerializeField]
    public RoomType roomType;

    [SerializeField]
    public int keysRequired;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = spawn.transform.position;

        //DEBUG
        monsterSpawnLocation = spawn.transform.position - new Vector3(10.0f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (Mathf.Abs((GameObject.Find("FPSController").transform.position - transform.position).magnitude) <= 5)
        //    {
        //            Activate();
        //    }
        //}
    }

    public void Activate()
    {
        if (GameObject.Find("FPSController").GetComponent<FPSPlayerBehaviour>().keyCount >= keysRequired)
        {
            GameObject.Find("RoomManager").GetComponent<RoomManager>().LoadRoom(roomType, spawnLocation, monsterSpawnLocation);
            //GameObject.Find("FPSController").GetComponent<CharacterController>().Move(spawnLocation);
            GameObject.Find("RoomManager").GetComponent<RoomManager>().InstantiatePlayer(spawnLocation);
            //GameObject.Find("FPSController").transform.position = spawnLocation;
        }
    }
}
