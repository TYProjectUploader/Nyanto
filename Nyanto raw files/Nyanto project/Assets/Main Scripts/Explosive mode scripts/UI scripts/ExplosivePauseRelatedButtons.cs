using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplosivePauseRelatedButtons : MonoBehaviour
{
    public static ExplosivePauseRelatedButtons instance;
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
        if (!Paused && !ExplosiveGameManager.instance.gameOver)
        {
            menuPrompts.SetActive(true);
            inGamePrompts.SetActive(false);

            AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
            CancelInvoke("hidePauseScreen");
            //if pause screen was set to be inactive but not yet cancel it to reduce rendering load
            pauseScreen.SetActive(true);
            pauseBoardUIAnimator.SetTrigger("Paused");
            Paused = true; //disable pause button
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
