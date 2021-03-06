using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleRaycast : MonoBehaviour
{
    /// <summary>
    /// UI Manager Script.
    /// </summary>
    [SerializeField] private UIManager uIManager;

    /// <summary>
    /// Reticle image asset.
    /// </summary>
    [SerializeField]
    Image reticle;

    /// <summary>
    /// Maximum distance from player at which Raycast can check for collisions.
    /// </summary>
    [SerializeField]
    private float maxDistance;

    /// <summary>
    /// Camera attached to the FirstPersonCharacter GameObject.
    /// </summary>
    [SerializeField]
    private Camera cam;

    /// <summary>
    /// Width of the reticle image asset.
    /// </summary>
    private float reticleWidth;

    /// <summary>
    /// Height of the reticle image asset.
    /// </summary>
    private float reticleHeight;

    /// <summary>
    /// x-coordinate of the centre of the screen.
    /// </summary>
    private float centreX;

    /// <summary>
    /// y-coordinate of the centre of the screen.
    /// </summary>
    private float centreY;

    /// <summary>
    /// out variable used to store information of objects the Raycast collides 
    /// with, if any.
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// Index of layer at which to check for collisions with the Raycast.
    /// </summary>
    private int layerIndex;

    /// <summary>
    /// Layer mask to Raycast to.
    /// </summary>
    private int layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        //Initializes the UIManager script
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        reticle = GameObject.Find("Canvas").transform.Find("Reticle").GetComponent<Image>();

        // Raycast from the centre of the screen, accounting for the reticle's
        // dimensions.
        reticleWidth = reticle.rectTransform.rect.width;
        reticleHeight = reticle.rectTransform.rect.height;
        centreX = Screen.width / 2 - reticleWidth / 2;
        centreY = Screen.width / 2 - reticleHeight / 2;
        
        // Get the index of the Interactables layer.
        layerIndex = LayerMask.NameToLayer("Interactables");

        // If the index does not exist, NameToLayer() returns -1. Handle this
        // error case explicitly.
        if (layerIndex == -1) Debug.LogError("Layer does not exist");

        // Otherwise, the index exists. Calculate the layer mask to Raycast to,
        // by bitshifting 1 by the layer's index to obtain a bitmask.
        else layerMask = 1 << layerIndex;
    }

    // Use FixedUpdate since a Raycast is a physics-related query.
    void FixedUpdate()
    {
        
    }

    public void Cast(bool isEnabled)
    {
        // Draw a ray for debugging purposes.
        //Debug.DrawRay(
        //    cam.ScreenToWorldPoint(new Vector3(centreX, centreY, 0)), 
        //    cam.transform.forward * maxDistance);

        // Using overload 15 of Physics.Raycast():
        // a) set the origin of the Raycast to be the centre of the screen;
        // b) the Raycast travels in the direction the camera is facing; and
        // c) check for collisions at the layer represented by the layer mask.
        bool raycastCollided = Physics.Raycast(
            cam.ScreenToWorldPoint(new Vector3(centreX, centreY, 0)),   // a
            cam.transform.forward,                                      // b
            out hit,
            maxDistance,
            layerMask);                                                 // c

        // If a collision is detected, and the object is the key, log a message
        // to the console.
        if (raycastCollided && hit.transform.CompareTag("Key"))
        {
            if (isEnabled)
            {
                Debug.Log("Found key");

                Destroy(hit.transform.gameObject);

                GameObject.Find("FPSController").GetComponent<FPSPlayerBehaviour>().keyCount++;
            }
            else
            {
                uIManager.SetReticleText("Key");
            }
        }
        else if (raycastCollided && hit.transform.CompareTag("Door"))
        {
            if (isEnabled)
            {
                hit.transform.gameObject.GetComponent<Room>().Activate();
            }
            else
            {
                uIManager.SetReticleText("Door");
            }
        }
        else
        {
            uIManager.SetReticleText("");
        }
    }
}
