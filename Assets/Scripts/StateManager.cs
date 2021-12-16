using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class StateManager : MonoBehaviour
{
    [SerializeField] public Canvas pauseCanvas;

    [SerializeField] public bool inGame;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }
        // Display and re-enable the cursor when a menu screen is displayed.
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //Pauses the game by disabling the time scale
    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseCanvas.gameObject.SetActive(isPaused);
        GameObject.Find("FPSController Variant").GetComponent<FirstPersonController>().isActive = isPaused;

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            return;
        }
        Time.timeScale = 0;
    }
}
