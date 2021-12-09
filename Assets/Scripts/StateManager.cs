using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public enum GameState
{
    Title,
    Game,
    End
}

public class StateManager : MonoBehaviour
{
    [SerializeField] public Canvas pauseCanvas;

    [SerializeField] public GameState gameState;

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
        if (gameState == GameState.Game)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                TogglePause();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

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
