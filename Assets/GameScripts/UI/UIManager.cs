using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [Header("Main Level")]
    public GameObject pauseMenu;
    public PlayerController playerController;

    [Header("Game Over")]
    public GameObject winBackground;
    public GameObject loseBackground;
    public TMP_Text result;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            SetGameOver();
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StartGame(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1.0f)
        {
            StageManager.Instance.isPaused = true;

            ToggleCursor();

            Time.timeScale = 0.0f;
        }
        else
        {
            StageManager.Instance.isPaused = false;

            ToggleCursor();

            Time.timeScale = 1.0f;
        }

        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }

    private void SetGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameManager.Instance.win)
        {
            winBackground.SetActive(true);
            result.text = "YOU WIN";
        }

        else
        {
            loseBackground.SetActive(true);
            result.text = "YOU LOSE";
        }

        GameManager.Instance.win = false;
    }

    public void ToggleCursor()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
