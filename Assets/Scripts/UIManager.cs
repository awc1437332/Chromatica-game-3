using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text raycastText;
    [SerializeField] private Text objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Sets the text display for the reticle to display text based on the unique item being targeted
    public void SetReticleText(string _tag)
    {
        switch (_tag)
        {
            case "Door":
                raycastText.text = "Open DOOR";
                break;
            case "Key":
                raycastText.text = "Grab KEY";
                break;
            case "":
                raycastText.text = "";
                break;
        }
    }

    public void SetObjectiveText(RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.Neutral:
                objectiveText.text = "";
                break;
            case RoomType.Puzzle:
                objectiveText.text = "Find the KEY and open the DOOR";
                break;
            case RoomType.Chase:
                objectiveText.text = "Find the EXIT";
                break;
        }
    }
}
