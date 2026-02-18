using System;
using UI_Scripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public InputActionReference pauseButton;
    private OutOfBounds _outOfBounds;
    
    void Awake()
    {
        pauseMenu.SetActive(false);
        _outOfBounds = transform.GetChild(0).gameObject.GetComponent<OutOfBounds>();
    }

    private void OnEnable()
    {
        pauseButton.action.started += ButtonWasPressed;
    }

    private void OnDisable()
    {
        pauseButton.action.started -= ButtonWasPressed;
    }

    void ButtonWasPressed(InputAction.CallbackContext context)
    {
        print("Button was pressed");
        TogglePause();
    }
    

    public void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        //timey wimey, wibbly wobbly stuff
        //bool ispaused = pauseMenu.activeSelf;
        //Time.timeScale = ispaused ? 0 : 1;
    }

    public void ResetPosition()
    {
        transform.position = _outOfBounds.lastPlayerPosition;
        transform.rotation = _outOfBounds.lastPlayerRotation;
        TogglePause();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    

    public async void QuitGame()
    {
        try
        {
            Time.timeScale = 0;
            SaveSystem.Save();
            await Awaitable.NextFrameAsync();
            print("quitting game");
            Application.Quit();
        }
        catch (Exception e)
        {
            print(e.Message);
            throw; // TODO handle exception
        }
    }
}
