using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Neutral,
    Puzzle,
    Chase
}

public class RoomManager : MonoBehaviour
{
    public GameObject playerObject;
    //private Player player;

    private RoomType currentType;

    // Start is called before the first frame update
    void Start()
    {
        currentType = RoomType.Puzzle;
        //player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads a room given the following parameters
    public void LoadRoom(RoomType _roomType, Vector3 _spawnLocation, Vector3 _monsterSpawnLocation)
    {
        //Sets the new room type
        currentType = _roomType;

        //Removes the player's keys from the previous room
        //player.RemoveKeys();

        //Toggles the monster and atmosphere based on the room type
        if (currentType != RoomType.Chase)
        {
            //Disable the monster
            //monster.isActive = false;

            //Adjust Atmosphere
        }
        else //Chase Room
        {
            //Enable and place the monster
            //monster.Activate(_monsterSpawnLocation); SET REFERENCE TO PLAYER

            //Adjust Atmosphere
        }
    }

    //Instantiates a player inside of a new room
    public void InstantiatePlayer(Vector3 _spawnLocation)
    {
        Destroy(GameObject.Find("FPSController"));
        GameObject newPlayer = Instantiate(playerObject, _spawnLocation, Quaternion.identity);
        newPlayer.name = "FPSController";
    }
}
