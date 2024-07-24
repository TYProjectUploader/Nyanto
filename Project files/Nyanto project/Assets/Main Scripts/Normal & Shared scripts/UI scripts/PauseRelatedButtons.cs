using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseRelatedButtons : MonoBehaviour
//buttons for the pause menu
{
    public static PauseRelatedButtons instance;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Animator pauseBoardUIAnimator;
    [SerializeField] private GameObject menuPrompts;
    [SerializeField] private GameObject inGamePrompts;
    public bool Paused = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // in terms of performance its most effective to keep the pause screen up at all times if pause/unpause is spammed by user
    public void PauseScreen()
    {
        Debug.Log("pause button pressed");
        if (!Paused && !GameManager.instance.gameOver)
        {
            //change out input prompts
            menuPrompts.SetActive(true);
            inGamePrompts.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
            //if pause screen was set to be inactive hasn't been hidden cancel it to reduce rendering load
            CancelInvoke("hidePauseScreen");

            //activate pause screen
            pauseScreen.SetActive(true);
            pauseBoardUIAnimator.SetTrigger("Paused");
            Paused = true; 
            Time.timeScale = 0;
        }
        else //let player resume by pressing the pause button again. 
        {
            Resume();
        }
    }
    public void Resume()
    {
        if (Paused)
        {
            menuPrompts.SetActive(false);
            inGamePrompts.SetActive(true);
            AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
            pauseBoardUIAnimator.SetTrigger("Resumed");
            Time.timeScale = 1;
            Invoke("hidePauseScreen", 2);
            //set pause sreen inactive to improve performance
            EventSystem.current.SetSelectedGameObject(null);
            Paused = false;
        }
    }
    private void hidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }
}
