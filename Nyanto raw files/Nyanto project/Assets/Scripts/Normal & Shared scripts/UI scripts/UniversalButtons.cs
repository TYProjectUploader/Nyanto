using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UniversalButtons : MonoBehaviour
{
    public void ReloadScene()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
        //reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadOpeningMenu()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
        //load home screen
        SceneManager.LoadScene("OpeningMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
